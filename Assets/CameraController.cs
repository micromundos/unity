using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

//	public GameObject target;
	private float positionSpeed =  0.01f;
	//private float rotationSpeed =  0.5f;

	//private Vector3 defaultPos;
	//private Vector3 defaultRot;

	private float _x_1;
	private float _y_1;
	private float _x_2;
	private float _y_2;
	private float _x_3;
	private float _y_3;
	private float _x_4;
	private float _y_4;

	public  MeshFilter  meshe;

	void Start()
	{
	//	defaultPos = target.transform.localPosition;
	//	defaultRot = target.transform.localEulerAngles;

		_x_1 = PlayerPrefs.GetFloat ("_x_1");
		_y_1 = PlayerPrefs.GetFloat ("_y_1");
		_x_2 = PlayerPrefs.GetFloat ("_x_2");
	    _y_2 = PlayerPrefs.GetFloat ("_y_2");
		_x_3 = PlayerPrefs.GetFloat ("_x_3");
		_y_3 = PlayerPrefs.GetFloat ("_y_3");
		_x_4 = PlayerPrefs.GetFloat ("_x_4");
		_y_4 = PlayerPrefs.GetFloat ("_y_4");
		Invoke ("saveData", 1);
	}
	void saveData()
	{
		PlayerPrefs.SetFloat ("_x_1", _x_1);
		PlayerPrefs.SetFloat ("_x_2", _x_2);
		PlayerPrefs.SetFloat ("_x_3", _x_3);
		PlayerPrefs.SetFloat ("_x_4", _x_4);
		PlayerPrefs.SetFloat ("_y_1", _y_1);
		PlayerPrefs.SetFloat ("_y_2", _y_2);
		PlayerPrefs.SetFloat ("_y_3", _y_3);
		PlayerPrefs.SetFloat ("_y_4", _y_4);
		Invoke ("saveData", 1);
	}

	void Update () {

		Vector3[] vertices = new Vector3[4];

		vertices[0] = new Vector3(_x_1, _y_1, 1);
		vertices[1] = new Vector3(_x_2, _y_2, 1);
		vertices[2] = new Vector3(_x_3, _y_3, 1);
		vertices[3] = new Vector3(_x_4, _y_4, 1);
		meshe.mesh.vertices = vertices;
		int[] tri = new int[6];

		//  Lower left triangle.
		tri[0] = 0;
		tri[1] = 2;
		tri[2] = 1;
		
		//  Upper right triangle.   
		tri[3] = 2;
		tri[4] = 3;
		tri[5] = 1;
		
		meshe.mesh.triangles = tri;	

		Vector2[] uv= new Vector2[4];
		
		uv[0] = new Vector2(0, 0);
		uv[1] = new Vector2(1, 0);
		uv[2] = new Vector2(0, 1);
		uv[3] = new Vector2(1, 1);
		
		meshe.mesh.uv = uv;


		if(Input.GetKeyDown(KeyCode.UpArrow))
			_x_1 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		    _x_1 -=  positionSpeed;
	
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		   _y_1 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		   _y_1 -=  positionSpeed;

		if(Input.GetKeyDown(KeyCode.A))
		   _x_2 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.D))
		   _x_2 -=  positionSpeed;

		if(Input.GetKeyDown(KeyCode.W))
			_y_2 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.S))
			_y_2 -=  positionSpeed;

		if(Input.GetKeyDown(KeyCode.F))
			_x_3 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.H))
			_x_3 -=  positionSpeed;
		
		if(Input.GetKeyDown(KeyCode.T))
			_y_3 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.G))
			_y_3 -=  positionSpeed;
		
		if(Input.GetKeyDown(KeyCode.J))
			_x_4 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.L))
			_x_4 -=  positionSpeed;
		
		if(Input.GetKeyDown(KeyCode.I))
			_y_4 +=  positionSpeed;
		else if(Input.GetKeyDown(KeyCode.K))
			_y_4 -=  positionSpeed;
		
		//		if (target.transform.localPosition.y != _y)
//			PlayerPrefs.SetFloat ("_y", _y);
//		if (target.transform.localPosition.x != _x)
//			PlayerPrefs.SetFloat ("_x", _x);
//		if (target.transform.localPosition.z != _z)
//			PlayerPrefs.SetFloat ("_z", _z);
//
//		target.transform.localPosition =  defaultPos +  new Vector3(_x, _y, _z);
//		target.transform.localEulerAngles = new Vector3 (defaultRot.x + _rot_x, 180, defaultRot.z + _rot_z);

	}
}
