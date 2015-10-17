using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    public Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        SetOnAgain();
    }
    public void Catched()
    {
        Invoke("SetOnAgain", 10);
        GetComponent<Collider>().enabled = false;
        anim.Play("starCatched");
    }
    void SetOnAgain()
    {
        anim.Play("starRestart");
        float _X = (float)Random.Range(-120, 120) / 100;
        float _Y = (float)Random.Range(-80, 80) / 100;
        Vector2 pos = new Vector2(_X, _Y);
        transform.localPosition = pos;

        GetComponent<Collider>().enabled = true;
        Invoke("loop", 1);
    }
    void loop()
    {
        anim.Play("starLoop");
    }
}
