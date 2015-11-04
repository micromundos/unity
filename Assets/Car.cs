using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public Sprite[] sprites;
    public float pendiente;
    public float speed;
    public float acceleration;
    public float maxSpeed;
    public Vector3 direction;
    public Vector3 rotation;
    public float smoothRotation;
    public float floorHeight;
    private bool debug;

    private Teletransportable teletransportable;

	public void Init (int id) {
        debug = GameManager.Instance.settings.DEBUG;
        teletransportable = GetComponent<Teletransportable>();
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[id];
	}

	void Update () {
        Vector3 pos = transform.position ;
        if (Mathf.Abs(pos.x) > 4 || Mathf.Abs(pos.y) > 4 ) Events.DestroyCar(this);

        Vector3 frontPosition = pos;
        frontPosition += transform.forward / 10;
        

        Vector3 leftPosition = (pos + transform.up/10) + transform.right / 10;
        CheckBorderHit(leftPosition, Color.blue, "left");

        Vector3 rightPosition = (pos + transform.up/10) - transform.right / 10;
        CheckBorderHit(rightPosition, Color.green, "right");

        speed += (acceleration);
        if (speed > maxSpeed) speed = maxSpeed;

        pos += transform.up * speed * Time.deltaTime;
        transform.position = pos;

        Vector3 rot = transform.localEulerAngles;
        rot = transform.localEulerAngles - rotation;

        CheckCenterHit(frontPosition, Color.red);
	}
    string lastHitObjectTag;
    void CheckCenterHit(Vector3 coord, Color DebugColor)
    {
        if (debug) DebugDraw.DrawSphere(coord, 0.1f, DebugColor);

        RaycastHit hit = GetCollision(coord, "center");
        if (hit.transform == null) return;
        switch (hit.transform.gameObject.tag)
        {
            case "Bomb":
                Events.DestroyCar(this);
                break;
            case "Star":
                Events.OnStarCatched(hit.transform.gameObject);
                break;
            case "Limite":
                Events.DestroyCar(this);
                //if (Mathf.Abs(transform.localEulerAngles.z - hit.transform.localEulerAngles.z) > 40)
                //    speed /= 1.2f;
                //transform.rotation = Quaternion.Slerp(transform.rotation, hit.transform.rotation, Time.deltaTime * smoothRotation);
                break;
            case "River":
                if (floorHeight<0.1f &&  lastHitObjectTag != hit.transform.gameObject.tag)
                {
                    Vector3 rot = transform.localEulerAngles;
                    rot.z = rot.z - 180;
                    transform.localEulerAngles = rot;
                }
                break;
            case "DoblaRandom":
                transform.rotation = Quaternion.Slerp(transform.rotation, hit.transform.rotation, Time.deltaTime * smoothRotation);
                break;
            case "Dobla":
                if (Mathf.Abs(transform.localEulerAngles.z - hit.transform.localEulerAngles.z) > 1)
                    speed /= 1.03f;
                transform.rotation = Quaternion.Slerp(transform.rotation, hit.transform.rotation, Time.deltaTime * smoothRotation);
                break;
            case "Tele":
                teletransportable.SetOn(hit.transform.localPosition);
                break;
            case "Cinta":
                Vector3 pos = transform.position;

                speed += (acceleration);
                if (speed > maxSpeed) speed = maxSpeed;

                pos += hit.transform.up * speed * Time.deltaTime;
                transform.position = pos;

                break;
        }
        lastHitObjectTag = hit.transform.gameObject.tag;
    }
    void CheckBorderHit(Vector3 coord, Color DebugColor, string positionName)
    {
        if (debug)
            DebugDraw.DrawSphere(coord, 0.1f, DebugColor);
        RaycastHit hit = GetCollision(coord, positionName);
    }
    private RaycastHit GetCollision(Vector3 coord, string positionName)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(coord, Vector3.down, 100.0F);
		if (hits.Length == 0)
			return new RaycastHit ();


        RaycastHit hitToReturn = hits[0];
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();
			//Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.name == "Plane"){
			//	Debug.Log(GameManager.Instance.GetFloorHeight(hit));
                changeFloorHeight(GameManager.Instance.GetFloorHeight(hit), positionName);
			}
            else hitToReturn = hit;
        }
        return hitToReturn;

       // RaycastHit hit = new RaycastHit();
        //Ray ray = new Ray();
        //ray.origin = coord;
        //ray.direction = Vector3.down;
        //if (Physics.RaycastAll(ray, out hit))
        //{
        //    if (hit.transform.gameObject.name == "Plane")
        //         changeFloorHeight(GameManager.Instance.GetFloorHeight(hit), positionName);

        //   // Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.point);
        //}
        //return hit;
    }
    void changeFloorHeight(float newFloorHeight, string positionName)
    {
		return;
        pendiente = newFloorHeight - floorHeight;
        if (newFloorHeight != floorHeight)
        {
            float heightDifference = Mathf.Abs(pendiente);
            if (heightDifference > 0.5f)
            {
                print("heightDifference " + heightDifference);
                if (positionName == "right") 
                    turn(true);
                else if (positionName == "left")
                    turn(false);
            } 
        }
        if (positionName == "center")
        {
            floorHeight = newFloorHeight;
            speed += pendiente / 1.5f;
        }
        
    }
    private void turn(bool right)
    {
        Vector3 angles = transform.localEulerAngles;

        if (right)
            angles.z -= 10;
        else
            angles.z += 10;

        transform.localEulerAngles = angles;
    }
}
