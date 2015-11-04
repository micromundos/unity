using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {

	//	public Camera camera;
	
	private Transform myTransform;
	private float positionSpeed =  1f;
	private float scaleSpeed = 1f;
	private float rotationSpeed =  1f;
	
	private Vector3 defaultPos;
	private Vector3 defaultRot;
	private Vector3 defaultScale;
	//  private float defaultFieldOfView;
	
	private float _x;
	private float _y;
	private float _z;
	private float _rot_x;
	private float _rot_y;
	private float _rot_z;
	private float _scale_x;
	
	//  private float _field_of_view;
	
	void Start()
	{
		myTransform = gameObject.transform;
		defaultPos = myTransform.localPosition;
		defaultRot = myTransform.localEulerAngles;
		 defaultScale = myTransform.localScale;
		
		_x = PlayerPrefs.GetFloat ("_x");
		_y = PlayerPrefs.GetFloat ("_y");
		_z = PlayerPrefs.GetFloat ("_z");
		_rot_x = PlayerPrefs.GetFloat("_rot_x");
		_rot_y = PlayerPrefs.GetFloat("_rot_y");
		_rot_z = PlayerPrefs.GetFloat("_rot_z");

		_scale_x = PlayerPrefs.GetFloat ("_scale_x");
		
		Invoke ("saveData", 1);
	}
	void saveData()
	{
		PlayerPrefs.SetFloat("_x", _x);
		PlayerPrefs.SetFloat("_y", _y);
		PlayerPrefs.SetFloat("_z", _z);
		PlayerPrefs.SetFloat("_rot_x", _rot_x);
		PlayerPrefs.SetFloat("_rot_x", _rot_y);
		PlayerPrefs.SetFloat("_rot_x", _rot_z);

		PlayerPrefs.SetFloat("_scale_x", _scale_x);
		Invoke ("saveData", 1);
	}
	
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			_x += positionSpeed;
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			_x -= positionSpeed;
		
		if(Input.GetKeyDown(KeyCode.UpArrow))
			_y +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.DownArrow))
			_y -= positionSpeed;		
		
		if(Input.GetKeyDown(KeyCode.Q))
			_z += positionSpeed;
		else if(Input.GetKeyDown(KeyCode.A))
			_z -= positionSpeed;
		
		
		
		if(Input.GetKeyDown(KeyCode.W))
			_rot_x += rotationSpeed;
		else if(Input.GetKeyDown(KeyCode.E))
			_rot_x -= rotationSpeed;
		
		if(Input.GetKeyDown(KeyCode.S))
			_rot_y += rotationSpeed;
		else if(Input.GetKeyDown(KeyCode.D))
			_rot_y -= rotationSpeed;
		
		if(Input.GetKeyDown(KeyCode.X))
			_rot_z += rotationSpeed;
		else if(Input.GetKeyDown(KeyCode.C))
			_rot_z -= rotationSpeed;
		
		if (Input.GetKeyDown(KeyCode.O))
		     _scale_x += scaleSpeed;
		 else if (Input.GetKeyDown(KeyCode.P))
			_scale_x -= scaleSpeed;

		
		myTransform.localPosition = defaultPos + new Vector3(_x, _y, _z);
		myTransform.localEulerAngles = new Vector3(defaultRot.x + _rot_x, defaultRot.y + _rot_y, defaultRot.z + _rot_z);
		myTransform.localScale = defaultScale + new Vector3 (_scale_x, 0, 0);
		
		// if (camera.fieldOfView != defaultFieldOfView + _field_of_view)
		//    camera.fieldOfView = defaultFieldOfView + _field_of_view;
		
	}
}

