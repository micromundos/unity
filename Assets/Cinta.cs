using UnityEngine;
using System.Collections;

public class Cinta : MonoBehaviour {

    public float scrollSpeed = 0.5F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = -Time.time * scrollSpeed;

        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
       //s Debug.Log(rend.material.GetTexture("_MainTex"));
        
    }
}
