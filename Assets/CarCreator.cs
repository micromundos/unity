using UnityEngine;
using System.Collections;

public class CarCreator : MonoBehaviour {

    public int carID;

	void Start () {
        CreateCar();
	}
    void CreateCar()
    {
        Invoke("CreateCar", 1);
        if (transform.localPosition.x > 6) return;
		Vector3 rot = transform.localEulerAngles;
		rot.x = 0;rot.y = 0;
		Events.AddNewCar(transform.localPosition, rot, carID);
        
    }
}
