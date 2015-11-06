using UnityEngine;
using System.Collections;

public class CLDepthMapManager : MonoBehaviour {

	// Use this for initialization

	private SyphonClientTexture	syphon_client;
	public SCalibrationLoader	calibration_loader;

	void Start () {



	}
	// Update is called once per frame
	void Update () {

		syphon_client = GetComponent<SyphonClientTexture> ();
		RenderTexture depth_texture = syphon_client.clientObject.AttachedTexture;
			//syphon_holder.GetComponent<Renderer> ().material.SetTexture ("_DepthTex", texture);
			GetComponent<Renderer> ().material.SetTexture ("depth_tex", depth_texture);
			GetComponent<Renderer> ().material.SetFloat ("width", calibration_loader.depth_cam_width);
			GetComponent<Renderer> ().material.SetFloat ("height", calibration_loader.depth_cam_height);
			GetComponent<Renderer> ().material.SetFloat ("near", calibration_loader.depth_cam_near);
			GetComponent<Renderer> ().material.SetFloat ("far", calibration_loader.depth_cam_far);
			GetComponent<Renderer> ().material.SetFloat ("cx", calibration_loader.depth_cam_cx);
			GetComponent<Renderer> ().material.SetFloat ("cy", calibration_loader.depth_cam_cy);
			GetComponent<Renderer> ().material.SetFloat ("fx", calibration_loader.depth_cam_fx);
			GetComponent<Renderer> ().material.SetFloat ("fy", calibration_loader.depth_cam_fy);
			GetComponent<Renderer> ().material.SetFloat ("tex_width", calibration_loader.tex_width);
			GetComponent<Renderer> ().material.SetFloat ("tex_height", calibration_loader.tex_height);
			GetComponent<Renderer> ().material.SetFloat ("far_clamp", calibration_loader.depth_cam_far_clamp);


	
	
	}


	

}
