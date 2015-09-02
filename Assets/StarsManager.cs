using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarsManager : MonoBehaviour {

    public List<GameObject> stars;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;

	void Start () {
        plane1.SetActive(false);
        plane2.SetActive(false);
        plane3.SetActive(false);
        Events.OnStarCatched += OnStarCatched;
	}
    void OnDestroy()
    {
        Events.OnStarCatched -= OnStarCatched;
    }
	void OnStarCatched(GameObject starGO)
    {
        print(starGO.name);

        starGO.GetComponent<Star>().Catched();

        GameObject plane = plane3;
        if (starGO.name == "Star1")
            plane = plane1;
        if (starGO.name == "Star2")
            plane = plane2;
        if (starGO.name == "Star3")
            plane = plane3;

        Vector3 starPos = new Vector3(starGO.transform.localPosition.x, 0, -2);
        plane.transform.localPosition = starPos;

        plane.SetActive(true);
        plane.GetComponent<Animation>().Play("Plane");

	}
}
