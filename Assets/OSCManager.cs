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


		if (simular) {

			if (packet.Address == "/add") {
				create_tag (packet as OSCMessage);
			} else if (packet.Address == "/remove") {
				int index = get_tag_by_id ((string)packet.Data [0]);
				if (index != -1) {
					tags.RemoveAt (index);
				}
			} else if (packet.Address == "/update") {
				int index = get_tag_by_id ((string)packet.Data [0]);
				if (index != -1) {
					(tags [index] as MMTag).x = (float)packet.Data [1];
					(tags [index] as MMTag).y = (float)packet.Data [2];
					(tags [index] as MMTag).angle = ((float)packet.Data [3])* Mathf.Rad2Deg;
				}
			}
		} else {
			if (packet.IsBundle ()) {
				foreach (OSCMessage o in packet.Data) {
					if (o.Address == "/add") {
						create_tag (o);
					} else if (o.Address == "/remove") {
						int index = get_tag_by_id ((string)o.Data [0]);
						if (index != -1) {
							tags.RemoveAt (index);
						}
					} else if (o.Address == "/update") {
						int index = get_tag_by_id ((string)o.Data [0]);
						if (index != -1) {
							(tags [index] as MMTag).x = (float)o.Data [1];
							(tags [index] as MMTag).y = (float)o.Data [2];
							(tags [index] as MMTag).angle = ((float)o.Data [3])* Mathf.Rad2Deg;


						}
					}
				}
			}
		}
	
	}

	void	create_tag(OSCMessage	_data)
	{
		MMTag _tag = new MMTag ();
		_tag.id = _data.Data [0].ToString();
		_tag.x = (float)_data.Data [1];
		_tag.y = (float)_data.Data [2];
		_tag.angle = ((float)_data.Data [3])* Mathf.Rad2Deg;
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
	public	float	x;
	public	float	y;
	public	float	angle;
	public	string	id;
	public  string  tag;

	public	MMTag()
	{
	}

}



