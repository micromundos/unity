﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class PController : MonoBehaviour {

    public Vector2 fixedPosition;
	public OSCManager	osc_manager;
	//public List<MMTag> ObjectsData;
	public List<ObjectData> ObjectsData;

    private float fixedPositionSpeed;
    private float fixedRotationSpeed;

    [Serializable]
    public class ObjectData
    {
        public int id;
        public int rotation;
        public Vector2 position;

        [HideInInspector]
        public string tag;
    }
    public List<SceneObject> objects;

    public GameObject ObjectsContiner;

	void Start()
	{
		fixedPositionSpeed = GetComponent<Settings>().fixedPositionSpeed;
		fixedRotationSpeed = GetComponent<Settings>().fixedRotationSpeed;
		/*
		foreach (ObjectData data in ObjectsData)
		{
			switch (data.id)
			{
			case 1: data.tag = "Creator"; break;
			case 2: data.tag = "Dobla"; break;
			case 3: data.tag = "Tele"; break;
			case 4: data.tag = "Cinta"; break;
			case 5: data.tag = "DoblaRandom"; break;
			case 6: data.tag = "Bomb"; break;
			case 7: data.tag = "River"; break;
			}
		}

		foreach (GameObject go in objects)
		{
			if (go.tag == data.tag && go.transform.localPosition.x > 6)
				return go;
		}
		return null;*/
	}



    string MapId(int id)
    {
        string tag = null;
        switch (id)
        {
            case 1: tag = "Creator"; break;
            case 2: tag = "Dobla"; break;
            case 3: tag = "Tele"; break;
            case 4: tag = "Cinta"; break;
            case 5: tag = "DoblaRandom"; break;
            case 6: tag = "Bomb"; break;
            case 7: tag = "River"; break;
        }
        return tag;
    }
    void Update()
    {
		ObjectsData.Clear ();
		foreach (MMTag data in osc_manager.tags) {
			ObjectData	o = new ObjectData();
			o.id = (int.Parse(data.id));
			o.position = data.position;
			o.rotation = (int)data.rotation;
			ObjectsData.Add(o);


		}
	//	ObjectsData = osc_manager.tags;


		
		foreach (ObjectData data in ObjectsData)
		{
			data.tag = MapId(data.id);
			SceneObject go = GetObjectByTag(data);
			
			if (!go) { Debug.Log("FALTA UN " + data.tag); return; }
			
			Vector3 fixedDataPosition = new Vector3(data.position.x * fixedPosition.x, data.position.y * fixedPosition.y, -0.2f);
			if (data.tag == "River")
				fixedDataPosition.z = 0;
			Vector3 newRotation = new Vector3(0, 0, data.rotation);
			
			Debug.Log(data.rotation);
			if (go.transform.localPosition.x > 6)
			{
				
				
				go.transform.localPosition = fixedDataPosition;
				if (data.tag != "DoblaRandom" && data.tag != "River")
					go.transform.localEulerAngles = newRotation;
			}
			else
			{
				go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, fixedDataPosition, fixedPositionSpeed);
				if (data.tag != "DoblaRandom" && data.tag != "River")
				{
					if (data.rotation > go.transform.localEulerAngles.z+1)
						go.transform.localEulerAngles = new Vector3(0, 0, go.transform.localEulerAngles.z + fixedRotationSpeed);
					else if (data.rotation < go.transform.localEulerAngles.z-1)
						go.transform.localEulerAngles = new Vector3(0, 0, go.transform.localEulerAngles.z - fixedRotationSpeed);
					// go.transform.localEulerAngles = newRotation;
					// go.transform.localEulerAngles = Vector3.Slerp(go.transform.localEulerAngles, newRotation, 0.05f);
				}
			}            
		}
		foreach (SceneObject go in objects)
		{
			if (go!= null && !go.inUse)
				go.transform.localPosition = new Vector3(10, 0, go.transform.localPosition.z);
			go.inUse = false;
		}


		osc_manager.Clear ();;
      
    }


	SceneObject GetObjectByTag(ObjectData data)
	{
		foreach (SceneObject go in objects)
		{
			if (go == null) print("NO EXISTE Con tag: " + data.tag);
			
			if (go!= null && go.tag == data.tag && go.inUse == false)
			{
				go.inUse = true;
				return go;
			}
		}
		print("ERROR " + data.tag);
		return null;
	}

    void CreateCar()
    {
        Events.AddNewCar(transform.localPosition, transform.localEulerAngles);
        Invoke("CreateCar", 2);
    }
	
}
