using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneObject : MonoBehaviour {

    public bool inUse;
    public int id;
    public bool disableRotation;


	public void SetPosition(Vector3 pos)
	{
		if (tag == "River")
			pos.z = 0;
		//transform.localPosition = pos;
		//return;
		if (pos.x > 2) {
			transform.localPosition = new Vector3(10,0,0); return;
		}
		Vector3 normPos = new Vector3 (pos.x, pos.y, pos.z);
		transform.localPosition =  Vector3.Lerp(transform.localPosition,normPos , 0.5f);

		//positions.Add(pos);
	}
	public void SetRotation(int rotation)
    {
		if (disableRotation)
			return;

		float f = Mathf.LerpAngle(transform.localEulerAngles.z, -rotation, 0.3f);
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, f);


    }

}
