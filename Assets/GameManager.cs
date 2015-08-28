using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public Car car;
    public GameObject container;
    public Settings settings;
    public SpriteRenderer piso;
    public Camera cam;
    static GameManager mInstance = null;
    public int totalCars;
	public SyphonParser	syphon_parser;

	float	delta_time;
	public	Text	text_field;

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
        cam = Camera.main;
		delta_time = 0.0f;
	}
    void OnDestroy()
    {
        Events.AddNewCar -= AddNewCar;
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

	void Update()
	{
		delta_time += (Time.deltaTime - delta_time) * 0.1f;
		float fps = 1.0f / delta_time;
		text_field.text = fps.ToString ("0.00");
	}

    void Updatessssssss() {
     
		if (!Input.GetMouseButton(0))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;
        
        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;
        
        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        print(tex.GetPixel((int)pixelUV.x, (int)pixelUV.y));
       // tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.red);
        //tex.Apply();
    }
}
