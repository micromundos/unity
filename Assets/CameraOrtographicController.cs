using UnityEngine;
using System.Collections;

public class CameraOrtographicController : MonoBehaviour {
	
		
		public Camera camera;
		
		private Transform myTransform;

		private float orthoSizeSpeed = 0.005f;
		private float speed =  0.0025f;
		private float speedRotation = 0.5f;

		private Vector3 defaultPos;
		private float defaultOrthoSize;
		
		private float _orthoSize;
		
		private float _x;
		private float _y;
		private float _rot_z;
		
		void Start()
		{
			myTransform = gameObject.transform;

			defaultPos = myTransform.localPosition;
			defaultOrthoSize = camera.orthographicSize;
			
			_x = PlayerPrefs.GetFloat ("_x",0);
			_y = PlayerPrefs.GetFloat ("_y",0);
			_rot_z = PlayerPrefs.GetFloat ("_rot_z",0);
			
			_orthoSize = PlayerPrefs.GetFloat ("_orthoSize");
			
			Invoke ("saveData", 1);
			Invoke ("timeout", 1);
		}
		
		bool loaded;
		void timeout()
		{
			loaded = true;
		}
		
		void saveData()
		{
			
			PlayerPrefs.SetFloat("_x", _x);
			PlayerPrefs.SetFloat("_y", _y);
			PlayerPrefs.SetFloat("_rot_z", _rot_z);
			
			PlayerPrefs.SetFloat("_orthoSize", _orthoSize);
			
			Invoke ("saveData", 1);
		}
		
		void Update () {
			
			if (!loaded)
				return;
			
			if (Input.GetKeyDown(KeyCode.UpArrow))
				_x += speed;
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			_x -= speed;
			
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			_y +=  speed;
			else if(Input.GetKeyDown(KeyCode.RightArrow))
			_y -= speed;		
			
			// Q-A ROTACION EJE Z
			if(Input.GetKeyDown(KeyCode.Q))
			_rot_z += speedRotation;
			else if(Input.GetKeyDown(KeyCode.A))
			_rot_z -= speedRotation;
			
			// W-S FRUSTUM STRETCH HORIZONTAL
			if (Input.GetKeyDown (KeyCode.W))
			_orthoSize += orthoSizeSpeed;
			else if(Input.GetKeyDown(KeyCode.S))
			_orthoSize -= orthoSizeSpeed;
			;
			
			
			if (Input.GetKeyDown (KeyCode.P)) {
				_x = defaultPos.x;
				_y = defaultPos.y;
				_orthoSize = defaultOrthoSize;
				_rot_z = 0;
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
			
			        myTransform.localPosition = defaultPos + new Vector3(_x, _y, 0);
				myTransform.localEulerAngles = new Vector3(0,0, _rot_z);
			
			// if (camera.fieldOfView != defaultFieldOfView + _field_of_view)
			//    camera.fieldOfView = defaultFieldOfView + _field_of_view;
			
			GetComponent<Camera> ().orthographicSize = defaultOrthoSize + _orthoSize;
			
			
		}
	}

