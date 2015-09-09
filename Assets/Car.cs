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

    private Teletransportable teletransportable;

	void Start () {
        teletransportable = GetComponent<Teletransportable>();
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[ Random.Range(0, sprites.Length) ];
	}

	void Update () {

        Vector3 pos = transform.position ;
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
    void CheckCenterHit(Vector3 coord, Color DebugColor)
    {
      //  DebugDraw.DrawSphere(coord, 0.1f, DebugColor);

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
                if (Mathf.Abs(transform.localEulerAngles.z - hit.transform.localEulerAngles.z) > 40)
                    speed /= 1.6f;
                transform.rotation = Quaternion.Slerp(transform.rotation, hit.transform.rotation, Time.deltaTime * smoothRotation);
                break;
            case "DoblaRandom":
                transform.rotation = Quaternion.Slerp(transform.rotation, hit.transform.rotation, Time.deltaTime * smoothRotation);
                break;
            case "Dobla":
                if (Mathf.Abs(transform.localEulerAngles.z - hit.transform.localEulerAngles.z) > 1)
                    speed /= 1.03f;
                transform.rotation = Quaternion.Slerp(transform.rotation, hit.transform.rotation, Time.deltaTime * smoothRotation);
                break;
            case "Tele1":
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
    }
    void CheckBorderHit(Vector3 coord, Color DebugColor, string positionName)
    {
        if(GameManager.Instance.settings.DEBUG)
            DebugDraw.DrawSphere(coord, 0.1f, DebugColor);
        RaycastHit hit = GetCollision(coord, positionName);
    }
    private RaycastHit GetCollision(Vector3 coord, string positionName)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray();
        ray.origin = coord;
        ray.direction = Vector3.down;
        if (Physics.Raycast(ray, out hit))
        {
            changeFloorHeight(GameManager.Instance.GetFloorHeight(hit), positionName);

            Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.point);
        }
        return hit;
    }
    void changeFloorHeight(float newFloorHeight, string positionName)
    {
        pendiente = newFloorHeight - floorHeight;
        if (newFloorHeight != floorHeight)
        {
            float heightDifference = Mathf.Abs(pendiente);
            if (heightDifference > 0.5f)
            {
                if (positionName == "right") 
                    turn(true);
                else if (positionName == "left")
                    turn(false);
            } 
        }
        if (positionName == "center")
        {
            floorHeight = newFloorHeight;
            speed -= pendiente / 1.5f;
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
