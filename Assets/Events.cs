using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action<Vector3, Vector3, int> AddNewCar = delegate { };
    public static System.Action<GameObject> OnStarCatched = delegate { };
    public static System.Action<Car> DestroyCar = delegate { }; 
}
