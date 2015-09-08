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
    public List<SceneObject> objects;

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
            case 6: tag = "Bomb"; break;
        }
        return tag;
    }
    void Update()
    {
        

        foreach (ObjectData data in ObjectsData)
        {
            data.tag = MapId(data.id);
            SceneObject go = GetObjectByTag(data);
            go.inUse = true;

            if (!go) { Debug.Log("FALTA UN " + data.tag); return; }

            go.inUse = true;

            Vector2 fixedDataPosition = new Vector2(data.position.x * fixedPosition.x, data.position.y * fixedPosition.y);
            Vector3 newRotation = new Vector3(0, 0, data.rotation);
            if (go.transform.localPosition.x > 6)
            {
                go.transform.localPosition = fixedDataPosition;
                if (data.tag != "DoblaRandom")
                    go.transform.localEulerAngles = newRotation;
            }
            else
            {
                go.transform.localPosition = Vector2.Lerp(go.transform.localPosition, fixedDataPosition, 0.05f);
                if (data.tag != "DoblaRandom")
                {
                    go.transform.localEulerAngles = newRotation;
                   // go.transform.localEulerAngles = Vector3.Slerp(go.transform.localEulerAngles, newRotation, 0.05f);
                }
            }
            

            
        }
        foreach (SceneObject go in objects)
        {
            if (!go.inUse)
                go.transform.localPosition = new Vector3(10, 0, 0);
            go.inUse = false;
        }
      
    }
    SceneObject GetObjectByTag(ObjectData data)
    {
        foreach (SceneObject go in objects)
        {
            if (go.tag == data.tag && !go.inUse)
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
