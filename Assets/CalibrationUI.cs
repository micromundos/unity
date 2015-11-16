using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalibrationUI : MonoBehaviour {

    public int TAG_1_ID;
    public int TAG_2_ID;
    public int TAG_3_ID;
    public int TAG_4_ID;

	public GameObject corner1;
    public GameObject corner2;
    public GameObject corner3;
    public GameObject corner4;

    public List<Vector2> originalPoints;
    public List<Vector2> calibratedPoints;

    public states state;
    public PController controller;

    public enum states
    {
        OFF,
        ON
    }

	void Start () {
        state = states.ON;

        calibratedPoints = new List<Vector2>();

        for (int a = 0; a < 4; a++)
        {
            originalPoints.Add(Vector2.zero);
            calibratedPoints.Add(Vector2.zero);
        }
        originalPoints[0] = corner1.transform.position;
        originalPoints[1] = corner2.transform.position;
        originalPoints[2] = corner3.transform.position;
        originalPoints[3] = corner4.transform.position;

	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ToogleState();

        if (state == states.ON)
        {
            foreach (PController.ObjectData data in controller.ObjectsData)
            {
                if (data.id == TAG_1_ID) SetPosition(1, data.position);
                else if (data.id == TAG_2_ID) SetPosition(2, data.position);
                else if (data.id == TAG_3_ID) SetPosition(3, data.position);
                else if (data.id == TAG_4_ID) SetPosition(4, data.position);
            }
            if (Input.GetKeyDown(KeyCode.Z))
                Ready();
        }
    }
    void Ready()
    {
        print("READY");
        ToogleState();
    }
    void SetPosition(int id, Vector2 pos)
    {
        calibratedPoints[id-1] =  pos;
    }
    void ToogleState()
    {
        if (state == states.ON)
        {
            state = states.OFF;
            Hide();
        }
        else
        {
            state = states.ON;
            Show();
        }
    }
    void Show()
    {
        corner1.SetActive(true);
        corner2.SetActive(true);
        corner3.SetActive(true);
        corner4.SetActive(true);
    }
    void Hide()
    {
        corner1.SetActive(false);
        corner2.SetActive(false);
        corner3.SetActive(false);
        corner4.SetActive(false);
    }
	

	
}
