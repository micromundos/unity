using UnityEngine;
using System.Collections;

public class Teletransportable : MonoBehaviour {

    public bool teletransporting;

	public void SetOn (Vector3 originalPosition) {

        if (teletransporting) return;
        teletransporting = true;
        
        GameObject destination = GetOtherTeletransport(originalPosition);
        if (destination == null) return;
        GetComponent<TrailRenderer>().time = 0;
       // destination.y = destination.z;
       
        Vector3 pos  = destination.transform.localPosition;
         pos.z = transform.localPosition.z;
         transform.localPosition = pos;

         transform.localEulerAngles = destination.transform.localEulerAngles;
         Invoke("SetOnTrail", 0.1f);
         Invoke("SetOff", 2);

        print("teletransporting from "  + originalPosition + " to: " + destination);
        
	}
    void SetOnTrail()
    {
        GetComponent<TrailRenderer>().time = 3;
    }

    private GameObject GetOtherTeletransport(Vector3 originalPosition)
    {
        Vector3 pos = Vector3.zero;
        foreach (GameObject tele in GameObject.FindGameObjectsWithTag("Tele"))
        {
            if (tele.transform.localPosition != originalPosition)
                return tele;
        }
        return null;
    }
    void SetOff()
    {
        
        teletransporting = false;
	}
}
