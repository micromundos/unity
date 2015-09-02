using UnityEngine;
using System.Collections;

public class Dobla : MonoBehaviour {

    public float speed = 1;

    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + speed);
    }
}
