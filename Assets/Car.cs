using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
	public Color[] colors;
	public Sprite[] sprites;
	public float pendiente;
	public float realSpeed;
	public float acceleration;
	public float maxSpeed;
	public Vector3 direction;
	public Vector3 rotation;
	public float smoothRotation;
	public float floorHeight;
	private bool debug;
	
	private float surfaceMaxHeight;
	private float car_Turn_180_lowerSpeed;
	private float pendiente_Speed;
	private float speed;
	private float pendienteFrena;

	private bool collision_RIGHT;
	private bool collision_FORWARD;
	private bool collision_LEFT;
	
	private Teletransportable teletransportable;
	
	private Vector3 forwardVector;

	public int id;
	
	public void Init(int id)
	{
		this.id = id;
		pendienteFrena= GameManager.Instance.settings.pendienteFrena;
		surfaceMaxHeight = GameManager.Instance.settings.surfaceMaxHeight;
		pendiente_Speed = GameManager.Instance.settings.pendiente_Speed;
		car_Turn_180_lowerSpeed = GameManager.Instance.settings.car_Turn_180_lowerSpeed;
		acceleration = GameManager.Instance.settings.car_acceleration;
		maxSpeed = GameManager.Instance.settings.car_max_speed;
		
		debug = GameManager.Instance.settings.DEBUG;
		teletransportable = GetComponent<Teletransportable>();
		GetComponentInChildren<SpriteRenderer>().sprite = sprites[id];
		GetComponentInChildren<TrailRenderer>().material.color = colors[id];
		speed = maxSpeed / 1.5f;
	}
	
	public Vector3 lastPos;
	private bool started;
	void Update()
	{
		if(Mathf.Abs(transform.localPosition.x)>6
		  ||
		   Mathf.Abs(transform.localPosition.y)>6 )
		{
			Events.DestroyCar(this);
			return;
		}
		collision_RIGHT = false;
		collision_FORWARD = false;
		collision_LEFT = false;

		Vector3 pos = transform.position;
		//if (Mathf.Abs(pos.x) > 4 || Mathf.Abs(pos.y) > 4 ) Events.DestroyCar(this);
		
		Vector3 frontPosition = pos;

		frontPosition += transform.forward / 10;
		
		Vector3 leftPosition = (pos + transform.up / 10) + transform.right / 7;
		CheckBorderHit(leftPosition, Color.blue, "left");
		
		Vector3 rightPosition = (pos + transform.up / 10) - transform.right / 7;
		CheckBorderHit(rightPosition, Color.green, "right");
		
		Vector3 forwardPosition = pos + transform.up / 4;
		CheckBorderHit(forwardPosition, Color.green, "forward");
		
		speed += (acceleration);
		if (speed > maxSpeed) speed = maxSpeed;
		
		pos += ((forwardVector / 5) * speed) * Time.deltaTime;
		
		realSpeed = Vector3.Distance(pos, transform.position) * 100;
		
		transform.position = pos;

		Vector3 rot = transform.localEulerAngles;
		rot = transform.localEulerAngles - rotation;
		
		CheckCenterHit(frontPosition, Color.red);

		if (started) {
			if (realSpeed < 0.08f && pendiente > 0) {
				turn (180);
				return;
			}
			if (collision_FORWARD) {
				if (collision_RIGHT)
					turn (-20);
				else if (collision_LEFT)
					turn (20);
				if (speed > 0.15)
					speed /= 1.4f;
			} else
		if (collision_RIGHT)
				turn (-20);
			else if (collision_LEFT)
				turn (20);
		}

		started = true;
	}
	string lastHitObjectTag;
	void CheckCenterHit(Vector3 coord, Color _DebugColor)
	{
		if (debug) DebugDraw.DrawSphere(coord, 0.1f, _DebugColor);
		
		RaycastHit hit = GetCollision(coord, "center");
		
		forwardVector = transform.up * speed;

		if (hit.transform == null) return;

		
		
		switch (hit.transform.gameObject.tag)
		{
		case "Counter":
			Counter counter = hit.transform.gameObject.GetComponent<Counter>();
			if(counter.CarId-1 == id)
			{
				counter.Add();
				Events.DestroyCar(this);
			}
			break;
		case "Bomb":
			speed *=10;
			//Events.DestroyCar(this);
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
			if (floorHeight < 0.1f && lastHitObjectTag != hit.transform.gameObject.tag)
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
			forwardVector += hit.transform.up*4;
			//Vector3 pos = transform.position;
			
			//speed += (acceleration);
			//if (speed > maxSpeed) speed = maxSpeed;
			
			//pos += hit.transform.up * speed * Time.deltaTime;
			//transform.position = pos;
			
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
			return new RaycastHit();
		
		
		RaycastHit hitToReturn = hits[0];
		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			Renderer rend = hit.transform.GetComponent<Renderer>();
			//	Debug.Log(hit.transform.gameObject.name);
			if (hit.transform.gameObject.name == "SyphonReceiver")
			{
				//Debug.Log(GameManager.Instance.GetFloorHeight(hit));
				float pixelHeight = GameManager.Instance.GetFloorHeight(hit);
				//	print ("Pixel hiehgt:  " +  pixelHeight +  "    pos:  " + transform.position);
				changeFloorHeight(pixelHeight, positionName);
			} else if (hit.transform.gameObject.tag != "Untagged")
				return hit;
			//else hitToReturn = hit;
		}
		return hitToReturn;
	}
	void changeFloorHeight(float checkingFloorHeight, string positionName)
	{
		if (positionName == "center")
		{
			floorHeight = checkingFloorHeight;
		}

			if (positionName == "forward")
			{
				pendiente = checkingFloorHeight - floorHeight;
				speed -= (pendiente/pendienteFrena) * pendiente_Speed;
			}
		float heightDifference = Mathf.Abs(floorHeight - checkingFloorHeight);
			//Debug.Log ("pendiente: " + pendiente + "  heightDifference:  " + heightDifference);
			if (heightDifference > surfaceMaxHeight)
			{
				if (positionName == "right")
					collision_RIGHT = true;
				else if (positionName == "left")
					collision_LEFT = true;
				else if (positionName == "forward")
					collision_FORWARD = true;
			}


		
	}
	private void turn(float _y)
	{
		Vector3 angles = transform.localEulerAngles;
			angles.z += _y;
		transform.localEulerAngles = angles;
	}
}