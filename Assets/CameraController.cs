using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera camera;

	private Transform myTransform;
//	private float defaultOrthoSizeSpeed = 0.1f;
	private float rotationSpeed =  0.1f;
   // private float defaultFieldOfViewSpeed = 0.1f;
//	private float rotationSpeed =  0.5f;

	//private float defaultOrthoSize;

//	private float _orthoSize;

	private Vector3 defaultRot;
//    private float defaultFieldOfView;

    private float rot_x;
	private float rot_y;
	private float rot_z;

  //  private float _field_of_view;

	Matrix4x4 proj;
	Matrix4x4 default_proj;
	private float frustum_speed = 0.01f;
	private float frustum_stretch_hor;
	private float frustum_displace_hor;
	private float frustum_stretch_vert;
	private float frustum_displace_vert;

	public SCalibrationLoader calibration;

	void Start()
	{
		myTransform = gameObject.transform;
		defaultRot = myTransform.localEulerAngles;
       // defaultFieldOfView = camera.fieldOfView;

		rot_x = PlayerPrefs.GetFloat ("rot_x",0);
		rot_y = PlayerPrefs.GetFloat ("rot_y",0);
		rot_z = PlayerPrefs.GetFloat ("rot_z",0);

		frustum_stretch_hor = PlayerPrefs.GetFloat ("frustum_stretch_hor",0);
		frustum_displace_hor = PlayerPrefs.GetFloat ("frustum_displace_hor",0);
		frustum_stretch_vert = PlayerPrefs.GetFloat ("frustum_stretch_vert",0);
		frustum_displace_vert = PlayerPrefs.GetFloat ("frustum_displace_vert",0);

		//_orthoSize = PlayerPrefs.GetFloat ("_orthoSize");

		Invoke ("saveData", 1);
		Invoke ("timeout", 1);
	}

	bool loaded;
	void timeout()
	{
		loaded = true;

		default_proj = camera.projectionMatrix;
	}

	void saveData()
	{

        PlayerPrefs.SetFloat("rot_x", rot_x);
        PlayerPrefs.SetFloat("rot_y", rot_y);
        PlayerPrefs.SetFloat("rot_z", rot_z);

		PlayerPrefs.SetFloat("frustum_stretch_hor", frustum_stretch_hor);
		PlayerPrefs.SetFloat("frustum_displace_hor", frustum_displace_hor);
		PlayerPrefs.SetFloat("frustum_stretch_vert", frustum_stretch_vert);
		PlayerPrefs.SetFloat("frustum_displace_vert", frustum_displace_vert);

	//	PlayerPrefs.SetFloat("_orthoSize", _orthoSize);

		Invoke ("saveData", 1);
	}

	void Update () {

		if (!loaded)
			return;

		if (Input.GetKeyDown(KeyCode.UpArrow))
			rot_x += rotationSpeed;
		else if (Input.GetKeyDown(KeyCode.DownArrow))
			rot_x -= rotationSpeed;

		if(Input.GetKeyDown(KeyCode.LeftArrow))
			rot_y +=  rotationSpeed;
		else if(Input.GetKeyDown(KeyCode.RightArrow))
			rot_y -= rotationSpeed;		

		// Q-A ROTACION EJE Z
		if(Input.GetKeyDown(KeyCode.Q))
			rot_z += rotationSpeed;
		else if(Input.GetKeyDown(KeyCode.A))
			rot_z -= rotationSpeed;

		// W-S FRUSTUM STRETCH HORIZONTAL
		if (Input.GetKeyDown (KeyCode.W))
			frustum_stretch_hor += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.S))
			frustum_stretch_hor -= frustum_speed;

		// E-D FRUSTUM DISPLACE HORIZONTAL
		if (Input.GetKeyDown (KeyCode.E))
			frustum_displace_hor += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.D))
			frustum_displace_hor -= frustum_speed;

		// R-F FRUSTUM STRETCH VERTICAL
		if(Input.GetKeyDown(KeyCode.R))
			frustum_stretch_vert += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.F))
			frustum_stretch_vert -= frustum_speed;

		// T-G FRUSTUM DISPLACE VERTICAL
		if(Input.GetKeyDown(KeyCode.T))
			frustum_displace_vert += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.G))
			frustum_displace_vert -= frustum_speed;


		if (Input.GetKeyDown (KeyCode.P)) {
			rot_x = 0;
			rot_y = 0;
			rot_z = 0;
			frustum_stretch_hor = 0;
			frustum_displace_hor = 0;
			frustum_stretch_vert = 0;
			frustum_displace_vert = 0;
		}

//		if (Input.GetKeyDown(KeyCode.O))
//            _field_of_view += defaultFieldOfViewSpeed;
//        else if (Input.GetKeyDown(KeyCode.P))
//            _field_of_view -= defaultFieldOfViewSpeed;

	//	if (Input.GetKeyDown(KeyCode.O))
	//		_orthoSize += defaultOrthoSizeSpeed;
	//	else if (Input.GetKeyDown(KeyCode.P))
	//		_orthoSize -= defaultOrthoSizeSpeed;

//		if (myTransform.localPosition.y != _y)
//            PlayerPrefs.SetFloat("_y", _y);
//        if (myTransform.localPosition.x != _x)
//            PlayerPrefs.SetFloat("_x", _x);
//        if (myTransform.localPosition.z != _z)
//            PlayerPrefs.SetFloat("_z", _z);

//        myTransform.localPosition = defaultPos + new Vector3(_x, _y, _z);
        myTransform.localEulerAngles = new Vector3(defaultRot.x + rot_x, defaultRot.y + rot_y, defaultRot.z + rot_z);

       // if (camera.fieldOfView != defaultFieldOfView + _field_of_view)
        //    camera.fieldOfView = defaultFieldOfView + _field_of_view;

		//GetComponent<Camera> ().orthographicSize = defaultOrthoSize + _orthoSize;

		proj = default_proj;
		proj.m00 = default_proj.m00 + frustum_stretch_hor;
		proj.m02 = default_proj.m02 + frustum_displace_hor;
		proj.m11 = default_proj.m11 + frustum_stretch_vert;
		proj.m12 = default_proj.m12 + frustum_displace_vert;
//		!@#$%*(?????
		camera.projectionMatrix = proj;


	}
}
