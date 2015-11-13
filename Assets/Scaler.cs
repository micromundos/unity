using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {

    void Update()
    {
        float angles = transform.localEulerAngles.z/360;
        angles += 0.36f;
        transform.localScale = new Vector2(angles/3, angles/3);
    }
}
