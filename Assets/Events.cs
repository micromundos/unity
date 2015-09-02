using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action<Vector3, Vector3> AddNewCar = delegate { };
    public static System.Action<GameObject> OnStarCatched = delegate { };
}
