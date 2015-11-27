using UnityEngine;
using System.Collections;

public class CLCalibration : MonoBehaviour {


	public SCalibrationLoader	calibration_loader;
	bool	calib_applied;

	// Use this for initialization
	void Start () {
	
		calib_applied = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (!calib_applied) {
			calib_applied = true;
			//Debug.Log("MATRIX " + GetComponent<Camera>().projectionMatrix.ToString());

//			GetComponent<Camera>().projectionMatrix = calibration_loader.cl_projection_matrix;

			//GL.modelview = calibration_loader.cl_model_view_matrix;
			//GetComponent<Camera>().worldToCameraMatrix = calibration_loader.cl_model_view_matrix;
			//GetComponent<Camera>().transform.

//			calibration_loader.proj_loc.x *= -1;
//			calibration_loader.proj_loc.y *= -1;
//			GetComponent<Camera>().transform.position = calibration_loader.proj_loc;
//			GetComponent<Camera>().transform.position = new Vector3(
//				calibration_loader.cl_model_view_matrix[12],
//				calibration_loader.cl_model_view_matrix[13],
//				calibration_loader.cl_model_view_matrix[14]);

//			GetComponent<Camera>().transform.forward = calibration_loader.proj_fwd;
//			GetComponent<Camera>().transform.forward = new Vector3(
//				calibration_loader.cl_model_view_matrix[8],
//				calibration_loader.cl_model_view_matrix[9],
//				calibration_loader.cl_model_view_matrix[10]);

//			GetComponent<Camera>().transform.up = calibration_loader.proj_up;
//			GetComponent<Camera>().transform.up = new Vector3(
//				calibration_loader.cl_model_view_matrix[4],
//				calibration_loader.cl_model_view_matrix[5],
//				calibration_loader.cl_model_view_matrix[6]);

//			GetComponent<Camera>().transform.right = calibration_loader.proj_left;
//			GetComponent<Camera>().transform.right = new Vector3(
//				calibration_loader.cl_model_view_matrix[0],
//				calibration_loader.cl_model_view_matrix[1],
//				calibration_loader.cl_model_view_matrix[2]);

//			Vector3 angs = GetComponent<Camera>().transform.eulerAngles;
//			angs.x *= -1;
//			GetComponent<Camera>().transform.eulerAngles = angs;

			GetComponent<Camera>().nearClipPlane = calibration_loader.depth_cam_near;
			GetComponent<Camera>().farClipPlane = calibration_loader.depth_cam_far;

			/*
			Debug.Log(">> MODEL VIEW << ");
			Debug.Log(GL.modelview);*/


		}
	}
}
