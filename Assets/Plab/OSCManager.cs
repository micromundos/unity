using UnityEngine;
using System.Collections;
using UnityOSC;
using System.Net;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class OSCManager : MonoBehaviour {
	
	OSCClient	osc_client;
	OSCServer	osc_server;
	//public List<MMTag>		tags;
	private List<MMTag>	_tags;
	public List<MMTag> tags{

		[MethodImpl(MethodImplOptions.Synchronized)]
		get {return _tags;}


	}
	public		bool	simular = false;
	public PController pController;
	public		float		RGB_WIDTH = 1024.0f;
	public		float		RGB_HEIGHT = 576.0f;
	public		SCalibrationLoader	calibration_loader;
	// Use this for initialization
	void Start () {
		_tags = new List<MMTag>();
		osc_server = new OSCServer (7000);
		osc_server.PacketReceivedEvent += OnPacketReceived;
		osc_server.Connect ();
	}




	// Update is called once per frame
	void Update () {


		
	}

	/*	CHILITAGS /tags PACKAGE FORMAT */
	//	id
	//	size
	// corner_0x
	// corner_0y
	// corner_1x
	// corner_1y
	// corner_2x
	// corner_2y
	// corner_3x
	// corner_3y
	// translation_x
	// translation_y
	// translation_z

	void OnPacketReceived(OSCServer server, OSCPacket packet)
	{

		_tags.Clear ();
		if (packet.IsBundle ()) {
			foreach (OSCMessage o in packet.Data) {
				if(o.Address == "/tags")
				{
					int t_len = (int) o.Data[0];
					int	p_len = o.Data.Count;
					int	tag_offset = p_len/t_len;
					for(int i=0;i<t_len;i++){
						make_tag(o,(i*tag_offset));
					}
				//	Debug.Log("Tag " + o.Data[0]);
				}
			}
		}
		//Debug.Log("------------------------------------------"+tags.Count);
		/*
		if (packet.IsBundle ()) {

			foreach (OSCMessage o in packet.Data) {
				//Debug.Log("Mensaje " + o.Address + " = " + o.Data[0].ToString());
				if (o.Address == "/add") {
					create_tag (o);
				} else if (o.Address == "/remove") {
					int index = get_tag_by_id ((string)o.Data [0]);
					if (index != -1) {
						Debug.Log ("Borro tag con id " + index.ToString ());
						
						tags.RemoveAt (index);
					}
				} else if (o.Address == "/update") {
					int index = get_tag_by_id ((string)o.Data [0]);
					
					if (index != -1) {
						(tags [index] as MMTag).position.x = Remap(((float)o.Data [1]),0.0f,1.0f,-1.0f,1.0f);
						(tags [index] as MMTag).position.y = Remap(((float)o.Data [2]),0.0f,1.0f,1.0f,-1.0f);
					//	Debug.Log("update:  x:::: " + (tags [index] as MMTag).position.x);
						(tags [index] as MMTag).rotation = ((float)o.Data [3])* Mathf.Rad2Deg;
						
					}else{
						create_tag (o);
					}
				}
			}
		}*/

		
	}


	public void	Clear()
	{
		//tags.Clear ();
	}
	
	float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	void	make_tag(OSCMessage	_msg,int	_tag_offset)
	{
		MMTag _tag = new MMTag ();
		_tag_offset ++;
		_tag.id = _msg.Data[_tag_offset].ToString();
		_tag.size = (float)_msg.Data [_tag_offset + 1];
		for (int i=0; i<4; i++) {
			int	_corner_offset = _tag_offset+2;
			int _current_corner_x = _corner_offset+(i*2);
			_tag.corners[i] = new Vector2();
			_tag.corners[i].Set((float)_msg.Data[_current_corner_x],(float)_msg.Data[_current_corner_x+1]);
		}
		Vector2[] normalized_corners = new Vector2[4];
		Vector2 dir = new Vector2 ();
		normalize_corners (_tag.corners, normalized_corners);
		_tag.corners = normalized_corners;
		tag_loc_normalized (_tag.corners, ref _tag.position);
		tag_dir_from_corners (_tag.corners, ref dir);
		Vector2 up = new Vector2 ();
		up.Set (0.0f, 1.0f);
		_tag.rotation = Vector2.Angle (dir, up);
		tweak_loc (ref _tag.position);
		_tag.position.x = Remap(_tag.position.x,0.0f,1.0f,-1.0f,1.0f);
		_tag.position.y = Remap(_tag.position.y,0.0f,1.0f,1.0f,-1.0f);
		_tags.Add (_tag);

		/*tloc.x *= rgb_width;
      tloc.y *= rgb_height;
      tloc.set( tweak_H.preMult( ofVec3f( tloc.x, tloc.y, 0 ) ) );
      tloc.x /= rgb_width;
      tloc.y /= rgb_height;*/
	}

	void	tweak_loc(ref Vector2	_loc)
	{
		_loc.x *= RGB_WIDTH;
		_loc.y *= RGB_HEIGHT;
		Vector3 _point = new Vector3 ();
		_point.x = _loc.x;
		_point.y = _loc.y;
		Vector3	_trans_point = calibration_loader.tags_matrix.MultiplyPoint(_point);
		_loc.x = _trans_point.x;
		_loc.y = _trans_point.y;
		_loc.x /= RGB_WIDTH;
		_loc.y /= RGB_HEIGHT;
	}

	void	normalize_corners(Vector2[]	_src,Vector2[]	_dst)
	{
		for (int i=0; i<_src.Length; i++) {
			_dst[i].Set(_src[i].x/RGB_WIDTH,_src[i].y/RGB_HEIGHT);
		}
	}

	void	tag_loc_normalized(Vector2[]	_corners,ref Vector2	_pos)
	{
		for (int i=0; i<_corners.Length; i++) {
			_pos.x+=_corners[i].x;
			_pos.y+=_corners[i].y;
		}
		_pos.x/=4.0f;
		_pos.y/=4.0f;

	}

	void	tag_dir_from_corners(Vector2[]	_corners,ref Vector2	_dir)
	{
		Vector2	c0,c1;
		c0 = new Vector2 ();
		c1 = new Vector2 ();
		c0.Set (_corners [0].x, _corners [0].y);
		c1.Set (_corners [1].x, _corners [1].y);
		_dir.Set (c0.x-c1.x, c0.y-c1.y);
		_dir.Normalize ();
	}

	void	create_tag(OSCMessage	_data)
	{
		MMTag _tag = new MMTag ();
		_tag.id = _data.Data [0].ToString();
		_tag.position.x = Remap(((float)_data.Data [1]),0.0f,1.0f,-1.0f,1.0f);
		_tag.position.y = Remap(((float)_data.Data [2]),0.0f,1.0f,1.0f,-1.0f);
		_tag.rotation = ((float)_data.Data [3])* Mathf.Rad2Deg;
		//Debug.Log ("Creo tag con id " + _data.Data [0].ToString ());
		_tags.Add (_tag);
	}
	
	
	
	int		get_tag_by_id(string	_id)
	{
		int ret = -1;
		int i = 0;
		while (i<_tags.Count && ret ==-1) {
			if((_tags[i] as MMTag).id == _id)
			{
				ret = i;
			}
			
			i++;
		}
		return ret;
	}
	
	void OnDestroy()
	{
		osc_server.PacketReceivedEvent -= OnPacketReceived;
	}
	
	
}

public class MMTag
{
	public Vector2	position;
	public	float	rotation;
	public	string	id;
	public  string  tag;
	public float 	size;
	public Vector2[]	corners;
	
	public	MMTag()
	{
		position = new Vector2 ();
		corners = new Vector2[4];
	}
	
}



