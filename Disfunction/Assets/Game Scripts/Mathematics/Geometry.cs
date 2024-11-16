using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour
{
    public static float GetDistance(Vector3 from, Vector3 to)
    {
       return Mathf.Sqrt( Square((from.x - to.x)) + Square((from.y - to.y)) + Square((from.z - to.z))  );
    }

    public static float Square(float number)
    {
        return number * number;
    }
}
