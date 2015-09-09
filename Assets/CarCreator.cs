using UnityEngine;
using System.Collections;

public class CarCreator : MonoBehaviour {

	void Start () {
        CreateCar();
	}
    void CreateCar()
    {
        Invoke("CreateCar", 1);
        if (transform.localPosition.x > 6) return;
        Events.AddNewCar(transform.localPosition, transform.localEulerAngles);
        
    }
}
