using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnChecker : MonoBehaviour
{

    public float maxDummiesInRange;

    public Vector3 getRandomPosition(Vector3 center, float rangeX, float spawnHeight, float rangeZ)
    {
        float x = Random.Range(center.x - rangeX, center.x + rangeX);
        float z = Random.Range(center.z - rangeZ, center.z + rangeZ);

        return new Vector3(x, spawnHeight, z);
    }

    public float objectChanceOfSpawning(GameObject obj, float rangeX, float rangeZ, float spawnRadius, float dummyCount)
    {
        Renderer objSize = obj.GetComponent<Renderer>();

        if (objSize == null)
        {
            Debug.LogError("Dummy prefab must have a Renderer component to calculate size.");
        }

        Vector3 size = objSize.bounds.size;

        float length = size.x;
        float breadth = size.z;

        Debug.Log(length + " and and and " + breadth);

/* 
        N E E D   T O   F I X   T H I S   P A R T  

        Vector3 dummySize = objSize.bounds.size;
        float objWidth = rangeX + spawnRadius;
        float objBreadth = rangeZ + spawnRadius;

        Debug.Log(dummySize.x + " and " + dummySize.z);

        float maxDummyX = rangeX / objWidth;
        float maxDummyZ = rangeZ / objBreadth;

        int maxDummiesX = Mathf.FloorToInt(rangeX / maxDummyX);
        int maxDummiesZ = Mathf.FloorToInt(rangeZ / maxDummyZ);

        maxDummiesInRange = (float)(maxDummiesX * maxDummiesZ);
*/
        return maxDummiesInRange;
    }

    public bool isPositionFree(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        return colliders.Length == 0;
    }
}
