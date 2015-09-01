using UnityEngine;
using System.Collections;

public class Dobla : MonoBehaviour {

    public Transform asset;

    void Update()
    {
        if(asset)
        asset.localEulerAngles = new Vector3(0, 0, asset.localEulerAngles.z+2);
    }
}
