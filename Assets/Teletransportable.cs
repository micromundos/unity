using UnityEngine;
using System.Collections;

public class Teletransportable : MonoBehaviour {

    public bool teletransporting;

	public void SetOn (Vector3 originalPosition) {

        if (teletransporting) return;
        teletransporting = true;
        
        Vector3 destination = GetOtherTeletransport(originalPosition);
       // destination.y = destination.z;
        destination.z = transform.localPosition.z;
        transform.localPosition = destination;
        
        Invoke("SetOff", 2);

        print("teletransporting from "  + originalPosition + " to: " + destination);
	}
    private Vector3 GetOtherTeletransport(Vector3 originalPosition)
    {
        Vector3 pos = Vector3.zero;
        foreach (GameObject tele in GameObject.FindGameObjectsWithTag("Tele"))
        {
            if (tele.transform.localPosition != originalPosition)
                return tele.transform.localPosition;
        }
        return pos;
    }
    void SetOff()
    {
        teletransporting = false;
	}
}
