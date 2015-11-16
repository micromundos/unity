using UnityEngine;
using System.Collections;

public class Counter : SceneObject {

//	public int id;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer> ().material.SetFloat ("_Cutoff", 0.5f);
	}
}
