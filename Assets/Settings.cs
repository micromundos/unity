using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
	
	public bool DEBUG;
	public int maxCars;

	public float pendienteFrena;
	public float surfaceMaxHeight;
	public float pendiente_Speed; // 1.5f;
	public float car_Turn_180_lowerSpeed; // = 0.27f;
	public float car_acceleration;// = 0.005f;
	public float car_max_speed; // = 2;
	
	public float fixedPositionSpeed;
	public float fixedRotationSpeed;

	public	float MIN_FLOOR_INPUT;
	public	float MAX_FLOOR_INPUT;
	
}

