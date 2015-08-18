using UnityEngine;
using System.Collections;

public class CarCreator : MonoBehaviour {

	void Start () {
        CreateCar();
	}
    void CreateCar()
    {
        Events.AddNewCar(transform.localPosition, transform.localEulerAngles);
        Invoke("CreateCar", 2);
    }
}
