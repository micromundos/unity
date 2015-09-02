using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class Controller : MonoBehaviour {

    public Vector2 fixedPosition;
    public List<ObjectData> ObjectsData;    

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
        foreach (ObjectData data in ObjectsData)
        {
            switch (data.id)
            {
                case 1: data.tag = "Creator"; break;
                case 2: data.tag = "Dobla"; break;
                case 3: data.tag = "Tele"; break;
                case 4: data.tag = "Cinta"; break;
                case 5: tag = "DoblaRandom"; break;
            }
        }
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
        }
        return tag;
    }
    void Update()
    {
        foreach (GameObject go in objects)
            go.transform.localPosition = new Vector3(10,0,0);

        foreach (ObjectData data in ObjectsData)
        {
            data.tag = MapId(data.id);
            GameObject go = GetObjectByTag(data);

            if (!go) { Debug.Log("FALTA UN " + data.tag); return; }

            Vector2 fixedDataPosition = new Vector2(data.position.x * fixedPosition.x, data.position.y * fixedPosition.y);
            go.transform.localPosition = fixedDataPosition;

            if(data.tag != "DoblaRandom")
                go.transform.localEulerAngles = new Vector3(0, 0, data.rotation);
        }
    }
    GameObject GetObjectByTag(ObjectData data)
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
