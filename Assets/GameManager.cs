using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Car car;
    public GameObject container;
    public Settings settings;
    public SpriteRenderer piso;
    public Camera cam;
    static GameManager mInstance = null;
	public SyphonParser	syphon_parser;
    public int totalCars;

    public static GameManager Instance
    {
        get
        {
            return mInstance;
        }
    }
	void Start () {
        mInstance = this;
        settings = GetComponent<Settings>();
        Events.AddNewCar += AddNewCar;
        Events.DestroyCar += DestroyCar;
        cam = Camera.main;
	}
    void OnDestroy()
    {
        Events.AddNewCar -= AddNewCar;
        Events.DestroyCar -= DestroyCar;
    }
    void DestroyCar(Car car)
    {
        totalCars--;
        Destroy(car.gameObject);
    }
    void AddNewCar(Vector3 _position, Vector3 _eulerAngles)
    {
        if (totalCars >= settings.maxCars) return;
        totalCars++;

        Car newCar = Instantiate(car) as Car;
        newCar.transform.SetParent(container.transform);
        _position.z = -1;
        newCar.transform.localPosition = _position;
        _eulerAngles.x = 0; _eulerAngles.y = 0;
        newCar.transform.localEulerAngles = _eulerAngles;
        newCar.transform.localScale = new Vector3(1,1,1);
        
	}
    public float GetFloorHeight(RaycastHit hit )
    {
		if (syphon_parser != null) {
			Vector2 pixelUV = hit.textureCoord;
			pixelUV.x *= syphon_parser.SurfaceMap().width;
			pixelUV.y *= syphon_parser.SurfaceMap().height;
			Color color = syphon_parser.SurfaceMap().GetPixel ((int)pixelUV.x, (int)pixelUV.y);
			
			return color.r + color.g + color.b;
			
		} else {
			return 0;
		}
    }
}
