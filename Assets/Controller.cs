using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Controller : MonoBehaviour {

    public List<MMTag> ObjectsData;
	public OSCManager	osc_manager;

    [Serializable]
    public class ObjectData
    {
        public int id;
        public int rotation;
        public Vector2 position;

        [HideInInspector]
        public string tag;
    }
    public List<GameObject> objects;

    public GameObject ObjectsContiner;

    void Start()
    {
		/*
        foreach (ObjectData data in ObjectsData)
        {
            switch (data.id)
            {
                case 1: data.tag = "Creator"; break;
                case 2: data.tag = "Dobla"; break;
                case 3: data.tag = "Tele"; break;
                case 4: data.tag = "Cinta"; break;
            }
        }*/
    }
    string MapId(int id)
    {
        string tag = null;
		Debug.Log ("TAG " + id.ToString ());
        switch (id)
        {
            case 1: tag = "Creator"; break;
            case 2: tag = "Dobla"; break;
            case 3: tag = "Tele"; break;
            case 4: tag = "Cinta"; break;
        }
        return tag;
    }
    void Update()
    {

		ObjectsData = osc_manager.tags;

        foreach (GameObject go in objects)
            go.transform.localPosition = new Vector3(10,0,0);

        foreach (MMTag data in ObjectsData)
        {
            data.tag = MapId(Int32.Parse(data.id));
            GameObject go = GetObjectByTag(data);

            if (!go) { Debug.Log("FALTA UN " + data.tag); return; }

            go.transform.localPosition = new Vector2(data.x,data.y);
            go.transform.localEulerAngles = new Vector3(0, 0, data.angle);
        }
    }
    GameObject GetObjectByTag(MMTag data)
    {
        foreach (GameObject go in objects)
        {
            if (go.tag == data.tag && go.transform.localPosition.x > 6)
                return go;
        }
        return null;
    }
    void CreateCar()
    {
        Events.AddNewCar(transform.localPosition, transform.localEulerAngles);
        Invoke("CreateCar", 2);
    }
	
}
