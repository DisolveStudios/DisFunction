using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SpawnType 
{
    MultipleDummy,
    SingleDummy
}

public class DummySpawnSystem : MonoBehaviour
{
    public float health;
    public float numOfDummies;
    public float total_dummy_count;

    [Header("Adjust dummy movement here")]
    public float movementRange;
    public float movementSpeed;
    public float spawnRadius;
    public float spawnRangeX;
    public float spawnHeight;
    public float spawnRangeZ;

    public bool moveDummies;

    public SpawnType spawnType;

    public SpawnChecker spawnChecker;
    public GameObject obj;
    public GameObject dummy;

    private void Update()
    {
        checkIfAlive();
    }

    public void spawnMultipleDummies(float health)
    {
        total_dummy_count = 0;

        while (total_dummy_count < numOfDummies)
        {
            Vector3 spawnPosition = spawnChecker.getRandomPosition(transform.position, spawnRangeX, spawnHeight, spawnRangeZ);

            if (spawnChecker.isPositionFree(spawnPosition, spawnRadius))
            {

                Debug.Log(spawnPosition.x + " and " + spawnPosition.z);

                dummy = Instantiate(obj, spawnPosition, obj.transform.rotation);

                this.health = health;

                Dummy dummyScript = dummy.GetComponent<Dummy>();

                if (!moveDummies)
                {
                    movementSpeed = 0;
                }
                if (dummyScript != null)
                {
                    dummyScript.movementRange = movementRange;
                    dummyScript.movementSpeed = movementSpeed;
                }
                total_dummy_count++;
            }
        }
    }

    public void spawnSingleDummy(float health, float movementDistance)
    {
        Vector3 spawnPosition = spawnChecker.getRandomPosition(transform.position, spawnRangeX, spawnHeight, spawnRangeZ);
        dummy = Instantiate(obj, spawnPosition, obj.transform.rotation);
        this.health = health;
        Dummy dummyScript = dummy.GetComponent<Dummy>();
        if (!moveDummies)
        {
            movementSpeed = 0;
        }
        if (dummyScript != null)
        {
            dummyScript.movementRange = movementRange;
            dummyScript.movementSpeed = movementSpeed;
        }
        total_dummy_count++;
    }

    public void checkIfAlive()
    {
        if(total_dummy_count == 0)
        {
            switch (spawnType) {
                case SpawnType.MultipleDummy:
                    spawnMultipleDummies(100f);
                    break;
                case SpawnType.SingleDummy:
                    spawnSingleDummy(100f, this.movementRange);
                    break;
            }
        }
    }
}
