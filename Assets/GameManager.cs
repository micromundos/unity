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
		syphon_parser = cam.GetComponent<SyphonParser>();
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
    void AddNewCar(Vector3 _position, Vector3 _eulerAngles, int id)
    {
        if (totalCars >= settings.maxCars) return;
        totalCars++;

        Car newCar = Instantiate(car) as Car;
        newCar.Init(id);
        newCar.transform.SetParent(container.transform);
        _position.z = -1;
        newCar.transform.localPosition = _position;
        _eulerAngles.x = 0; _eulerAngles.y = 0;
        newCar.transform.localEulerAngles = _eulerAngles;
        newCar.transform.localScale = new Vector3(1,1,1);
        
	}
    public float GetFloorHeight(RaycastHit hit )
    {

       // return 0;


        if (syphon_parser != null && syphon_parser.SurfaceMap() != null) {

            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= syphon_parser.SurfaceMap().width;
            pixelUV.y *= syphon_parser.SurfaceMap().height;
            if((int)pixelUV.x > -1 && (int)pixelUV.x < syphon_parser.SurfaceMap().width && (int)pixelUV.y > -1 && (int)pixelUV.y < syphon_parser.SurfaceMap().height){

                Color color = syphon_parser.SurfaceMap().GetPixel ((int)pixelUV.x, (int)pixelUV.y);
                return color.r + color.g + color.b;
            }else{
                return 0;
            }
			
        } else {
            return 0;
        }

    }
}
