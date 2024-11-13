using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnChecker : MonoBehaviour
{
    public Vector3 getRandomPosition(Vector3 center, float rangeX, float rangeZ)
    {
        float x = Random.Range(center.x - rangeX, center.x + rangeX);
        float z = Random.Range(center.z - rangeZ, center.z + rangeZ);
        return new Vector3(x, 1.5f, z);
    }

    public static bool IsPositionFree(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        return colliders.Length == 0;
    }
}
