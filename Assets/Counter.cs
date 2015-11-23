using UnityEngine;
using System.Collections;

public class Counter : SceneObject {

	public SpriteRenderer asset;
	public int qty;
	public int CarId;
	public Color[] colors;
	public TextMesh[] fields;
	private Animation anim;

	void Start () {
		anim = GetComponent<Animation> ();
		foreach(TextMesh field in fields)
			field.color = colors [CarId - 1];

		asset.color = colors [CarId - 1];
	}
	void SetText(){
		foreach(TextMesh field in fields)
			field.text = qty.ToString ();
	}
	public void Add()
	{
		anim.Play ("counter");
		print ("addddd");
		qty++;
		SetText ();
	}
}
