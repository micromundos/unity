﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SyphonParser : MonoBehaviour {

	//	in
	public	GameObject	syphon_holder;	//el objeto que tenga el script de syphon asociado
	public	Slider		slider_fps;
	public  Text		text_slider;
	//	out
	private Texture2D	surface_map;
	bool seted;
	public Camera	cam;
	// Use this for initialization
	void Start () {
		surface_map = null;
		seted = false;
	}
	
	// Update is called once per frame
	void OnPostRender () {

		if (syphon_holder.GetComponent<SyphonClientTexture> ().clientObject == null)
			return;
		RenderTexture texture = syphon_holder.GetComponent<SyphonClientTexture> ().clientObject.AttachedTexture;
		if (texture != null) {
			if(texture.width > 0 && texture.height > 0){
				RenderTexture currentActiveRT = RenderTexture.active;
				RenderTexture.active = texture;
				if(surface_map == null){
					Debug.Log("ALOCO");
					surface_map = new Texture2D (texture.width, texture.height);
				}
				surface_map.ReadPixels (new Rect (0, 0, Mathf.RoundToInt(texture.width),  Mathf.RoundToInt(texture.height)), 0, 0);
				surface_map.Apply();
				RenderTexture.active = currentActiveRT;
			}else{
		//		surface_map = null;
			}
		} else {
			//surface_map = null;
		}		
	
	}



	public Texture2D	SurfaceMap()
	{
		return surface_map;
	}

}