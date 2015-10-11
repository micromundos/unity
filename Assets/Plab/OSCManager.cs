using UnityEngine;
using System.Collections;
using UnityOSC;
using System.Net;
using System.Collections.Generic;

public class OSCManager : MonoBehaviour {
	
	OSCClient	osc_client;
	OSCServer	osc_server;
	public List<MMTag>		tags;
	public		bool	simular = false;
	
	// Use this for initialization
	void Start () {
		tags = new List<MMTag>();
		osc_server = new OSCServer (9000);
		osc_server.PacketReceivedEvent += OnPacketReceived;
		osc_server.Connect ();
	}
	
	// Update is called once per frame
	void Update () {


		
	}
	
	void OnPacketReceived(OSCServer server, OSCPacket packet)
	{



		if (packet.IsBundle ()) {
			foreach (OSCMessage o in packet.Data) {
				Debug.Log("Mensaje " + o.Address + " = " + o.Data[0].ToString());
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
						(tags [index] as MMTag).rotation = ((float)o.Data [3])* Mathf.Rad2Deg;
						
					}else{
						Debug.Log("En update");
						create_tag (o);
					}
				}
			}
		}

		
	}

	public void	Clear()
	{
		tags.Clear ();
	}
	
	float Remap (this float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
	
	void	create_tag(OSCMessage	_data)
	{
		MMTag _tag = new MMTag ();
		_tag.id = _data.Data [0].ToString();
		_tag.position.x = Remap(((float)_data.Data [1]),0.0f,1.0f,-1.0f,1.0f);
		_tag.position.y = Remap(((float)_data.Data [2]),0.0f,1.0f,1.0f,-1.0f);
		_tag.rotation = ((float)_data.Data [3])* Mathf.Rad2Deg;
		Debug.Log ("Creo tag con id " + _data.Data [0].ToString ());
		tags.Add (_tag);
	}
	
	
	
	int		get_tag_by_id(string	_id)
	{
		int ret = -1;
		int i = 0;
		while (i<tags.Count && ret ==-1) {
			if((tags[i] as MMTag).id == _id)
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
	
	public	MMTag()
	{
	}
	
}



