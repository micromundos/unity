using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SyphonParser : MonoBehaviour {

	//	in
	public	GameObject	syphon_holder;	//el objeto que tenga el script de syphon asociado
	public	Slider		slider_fps;
	public  Text		text_slider;
	//	out
	private Texture2D	surface_map;

	// Use this for initialization
	void Start () {
		surface_map = null;
	}
	
	// Update is called once per frame
	void Update () {

		if (slider_fps.value == 0 || (slider_fps.value > 0 && Time.frameCount % 2 == slider_fps.value)) {

			RenderTexture texture = syphon_holder.GetComponent<SyphonClientTexture> ().clientObject.AttachedTexture;
			RenderTexture currentActiveRT = RenderTexture.active;
			RenderTexture.active = texture;
			surface_map = new Texture2D (texture.width, texture.height);
			surface_map.ReadPixels (new Rect (0, 0, texture.width, texture.height), 0, 0);
			RenderTexture.active = currentActiveRT;
		
		}

		text_slider.text = slider_fps.value.ToString ("0");
	}

	public Texture2D	SurfaceMap()
	{
		return surface_map;
	}

}
