﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneObject : MonoBehaviour {

    public bool inUse;
	private List<int> rotations;
    public int id;
    public bool disableRotation;

	void Start () {
		Invoke ("UpdataState", 1);
		rotations = new List<int> ();
	}
	void UpdataState()
	{
		Invoke ("UpdataState", 1);
		int rotationNormal = 0;
		int sumas = 0;

		return;
        if (disableRotation) return;

        
        else if (rotations.Count > 1)
        {
            foreach (int rot in rotations)
                sumas += rot;
            rotationNormal = sumas / rotations.Count;
            transform.localEulerAngles = new Vector3(0, 0, rotationNormal);
            rotations.Clear();
        }
	}
	public void SetPosition(Vector3 pos)
	{
		if (tag == "River")
			pos.z = 0;
		transform.localPosition = pos;
	}
	public void SetRotation(int rotation)
    {
		if(tag !="DoblaRandom" && tag !="River")
		transform.localEulerAngles = new Vector3(0, 0, -rotation);
		//rotations.Add(rotation);
    }

}
