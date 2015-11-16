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
  //  private float defaultFieldOfView;

	private float varsSpeed=0.01f;

//	public float var1_;
//	public float var2_;
//	public float var3_;
//	public float var4_;
//
//	public float var1;
//	public float var2;
//	public float var3;
//	public float var4;


    private float _x;
	private float _y;
	private float _z;
//    private float _rot_x;
//    private float _rot_y;
//    private float _rot_z;

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

		_x = PlayerPrefs.GetFloat ("_x",0);
		_y = PlayerPrefs.GetFloat ("_y",0);
		_z = PlayerPrefs.GetFloat ("_z",0);

		frustum_stretch_hor = PlayerPrefs.GetFloat ("frustum_stretch_hor",0);
		frustum_displace_hor = PlayerPrefs.GetFloat ("frustum_displace_hor",0);
		frustum_stretch_vert = PlayerPrefs.GetFloat ("frustum_stretch_vert",0);
		frustum_displace_vert = PlayerPrefs.GetFloat ("frustum_displace_vert",0);

		//_orthoSize = PlayerPrefs.GetFloat ("_orthoSize");

//        _rot_x = PlayerPrefs.GetFloat("_rot_x");
//        _rot_y = PlayerPrefs.GetFloat("_rot_y");
//        _rot_z = PlayerPrefs.GetFloat("_rot_z");

		Invoke ("saveData", 1);
		Invoke ("timeout", 1);
	}

	bool loaded;
	void timeout()
	{
		loaded = true;

//		var1 = calibration.tags_matrix.m00;
//		var2 = calibration.tags_matrix.m01;
//		var3 = calibration.tags_matrix.m10;
//		var4 = calibration.tags_matrix.m11;

		default_proj = camera.projectionMatrix;
	}

	void saveData()
	{

        PlayerPrefs.SetFloat("_x", _x);
        PlayerPrefs.SetFloat("_y", _y);
        PlayerPrefs.SetFloat("_z", _z);

		PlayerPrefs.SetFloat("frustum_stretch_hor", frustum_stretch_hor);
		PlayerPrefs.SetFloat("frustum_displace_hor", frustum_displace_hor);
		PlayerPrefs.SetFloat("frustum_stretch_vert", frustum_stretch_vert);
		PlayerPrefs.SetFloat("frustum_displace_vert", frustum_displace_vert);

	//	PlayerPrefs.SetFloat("_orthoSize", _orthoSize);
//        PlayerPrefs.SetFloat("_rot_x", _rot_x);
//        PlayerPrefs.SetFloat("_rot_x", _rot_y);
//        PlayerPrefs.SetFloat("_rot_x", _rot_z);

		Invoke ("saveData", 1);
	}

	void Update () {

		if (!loaded)
			return;

		if (Input.GetKeyDown(KeyCode.UpArrow))
			_x += rotationSpeed;
		else if (Input.GetKeyDown(KeyCode.DownArrow))
			_x -= rotationSpeed;

		if(Input.GetKeyDown(KeyCode.LeftArrow))
			_y +=  rotationSpeed;
		else if(Input.GetKeyDown(KeyCode.RightArrow))
			_y -= rotationSpeed;		

		// Q-A ROTACION EJE Z
		if(Input.GetKeyDown(KeyCode.Q))
			_z += rotationSpeed;
		else if(Input.GetKeyDown(KeyCode.A))
			_z -= rotationSpeed;

		// W-S FRUSTUM STRETCH HORIZONTAL
		if (Input.GetKeyDown (KeyCode.W))
//			var1_ += varsSpeed;
			frustum_stretch_hor += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.S))
//			var1_ -= varsSpeed;
			frustum_stretch_hor -= frustum_speed;

		// E-D FRUSTUM DISPLACE HORIZONTAL
		if (Input.GetKeyDown (KeyCode.E))
//			var2_ += varsSpeed;
			frustum_displace_hor += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.D))
//			var2_ -= varsSpeed;
			frustum_displace_hor -= frustum_speed;

		// R-F FRUSTUM STRETCH VERTICAL
		if(Input.GetKeyDown(KeyCode.R))
//			var3_+=varsSpeed;
			frustum_stretch_vert += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.F))
//			var3_-=varsSpeed;
			frustum_stretch_vert -= frustum_speed;

		// T-G FRUSTUM DISPLACE VERTICAL
		if(Input.GetKeyDown(KeyCode.T))
//			var4_ += varsSpeed;
			frustum_displace_vert += frustum_speed;
		else if(Input.GetKeyDown(KeyCode.G))
//			var4_ -= varsSpeed;
			frustum_displace_vert -= frustum_speed;


		if (Input.GetKeyDown (KeyCode.P)) {
			_x = 0;
			_y = 0;
			_z = 0;
			frustum_stretch_hor = 0;
			frustum_displace_hor = 0;
			frustum_stretch_vert = 0;
			frustum_displace_vert = 0;
		}


//		calibration.tags_matrix.m00 = var1_ + var1;
//		calibration.tags_matrix.m01 = var2_ + var2;
//		calibration.tags_matrix.m10 = var3_ + var3;
//		calibration.tags_matrix.m11 = var4_ + var4;

//		if(Input.GetKeyDown(KeyCode.W))
//			_rot_x += rotationSpeed;
//		else if(Input.GetKeyDown(KeyCode.E))
//			_rot_x -= rotationSpeed;
//		
//		if(Input.GetKeyDown(KeyCode.S))
//			_rot_y += rotationSpeed;
//		else if(Input.GetKeyDown(KeyCode.D))
//			_rot_y -= rotationSpeed;
//		
//		if(Input.GetKeyDown(KeyCode.X))
//			_rot_z += rotationSpeed;
//		else if(Input.GetKeyDown(KeyCode.C))
//			_rot_z -= rotationSpeed;
		
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
        myTransform.localEulerAngles = new Vector3(defaultRot.x + _x, defaultRot.y + _y, defaultRot.z + _z);

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
