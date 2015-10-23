using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera camera;

	private float positionSpeed =  0.005f;
    private float defaultFieldOfViewSpeed = 0.1f;
	private float rotationSpeed =  0.05f;

	private Vector3 defaultPos;
	private Vector3 defaultRot;
    private float defaultFieldOfView;

    private float _x;
	private float _y;
	private float _z;
    private float _rot_x;
    private float _rot_y;
    private float _rot_z;

    private float _field_of_view;

	void Start()
	{
        defaultPos = camera.transform.localPosition;
        defaultRot = camera.transform.localEulerAngles;
        defaultFieldOfView = camera.fieldOfView;

		_x = PlayerPrefs.GetFloat ("_x");
		_y = PlayerPrefs.GetFloat ("_y");
		_z = PlayerPrefs.GetFloat ("_z");
        _rot_x = PlayerPrefs.GetFloat("_rot_x");
        _rot_y = PlayerPrefs.GetFloat("_rot_y");
        _rot_z = PlayerPrefs.GetFloat("_rot_z");

        _x = PlayerPrefs.GetFloat("_x");
        _y = PlayerPrefs.GetFloat("_y");

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
            _rot_x += positionSpeed;
		else if(Input.GetKeyDown(KeyCode.E))
            _rot_x -= positionSpeed;

		if(Input.GetKeyDown(KeyCode.S))
            _rot_y += positionSpeed;
		else if(Input.GetKeyDown(KeyCode.D))
            _rot_y -= positionSpeed;
		
		if(Input.GetKeyDown(KeyCode.X))
            _rot_z += positionSpeed;
		else if(Input.GetKeyDown(KeyCode.C))
            _rot_z -= positionSpeed;

        if (Input.GetKeyDown(KeyCode.O))
            _field_of_view += defaultFieldOfViewSpeed;
        else if (Input.GetKeyDown(KeyCode.P))
            _field_of_view -= defaultFieldOfViewSpeed;

        if (camera.transform.localPosition.y != _y)
            PlayerPrefs.SetFloat("_y", _y);
        if (camera.transform.localPosition.x != _x)
            PlayerPrefs.SetFloat("_x", _x);
        if (camera.transform.localPosition.z != _z)
            PlayerPrefs.SetFloat("_z", _z);

        camera.transform.localPosition = defaultPos + new Vector3(_x, _y, _z);
        camera.transform.localEulerAngles = new Vector3(defaultRot.x + _rot_x, defaultRot.y + _rot_y, defaultRot.z + _rot_z);

        if (camera.fieldOfView != defaultFieldOfView + _field_of_view)
            camera.fieldOfView = defaultFieldOfView + _field_of_view;

	}
}
