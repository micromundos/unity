using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneObject : MonoBehaviour {

    public bool inUse;
	private List<int> rotations;

	void Start () {
		Invoke ("UpdataState", 1);
		rotations = new List<int> ();
	}
	void UpdataState()
	{
		Invoke ("UpdataState", 1);
		int rotationNormal = 0;
		int sumas = 0;
		if (rotations.Count == 0)
			return;
		foreach (int rot in rotations)
			sumas += rot;
		rotationNormal = sumas / rotations.Count;
		transform.localEulerAngles = new Vector3(0,0,rotationNormal);
		rotations.Clear();
	}
	public void SetRotation(int rotation)
	{
		rotations.Add (rotation);
	}

}
