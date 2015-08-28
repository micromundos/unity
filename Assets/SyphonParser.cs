using UnityEngine;
using System.Collections;

public class SyphonParser : MonoBehaviour {

	//	in
	public	GameObject	syphon_holder;	//el objeto que tenga el script de syphon asociado
	//	out
	private Texture2D	surface_map;

	// Use this for initialization
	void Start () {
		surface_map = null;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.frameCount % 3 == 0) {

			RenderTexture texture = syphon_holder.GetComponent<SyphonClientTexture> ().clientObject.AttachedTexture;
			RenderTexture currentActiveRT = RenderTexture.active;
			RenderTexture.active = texture;
			surface_map = new Texture2D (texture.width, texture.height);
			surface_map.ReadPixels (new Rect (0, 0, texture.width, texture.height), 0, 0);
			RenderTexture.active = currentActiveRT;
	

		}

	}

	public Texture2D	SurfaceMap()
	{
		return surface_map;
	}

}
