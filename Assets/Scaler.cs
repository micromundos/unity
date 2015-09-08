using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {

    void Update()
    {
        float angles = transform.localEulerAngles.z/600;
        angles += 0.14f;
        transform.localScale = new Vector2(angles, angles);
    }
}
