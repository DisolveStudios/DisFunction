using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SpawnType 
{
    MultipleDummy,
    SingleDummy
}

public class DummyDamageSystem : MonoBehaviour
{

    public GameObject obj;
    public GameObject dummy;

    public SpawnChecker spawnChecker;
    public SpawnType spawnType;
    public float health;
    public float numOfDummies;
    public float total_dummy_count;
    public Transform spawnCenter;
    public float spawnRadius = 1.5f;
    public bool moveDummies;
    public float movementDistance;


    private void Update()
    {
        checkIfAlive();
    }

    public void spawnMultipleDummies(float health)
    {
        total_dummy_count = 0;
        while (total_dummy_count < numOfDummies)
        {
            Vector3 spawnPosition = spawnChecker.getRandomPosition(spawnCenter.position, 10f, -5f);

            if (SpawnChecker.IsPositionFree(spawnPosition, spawnRadius))
            {
                dummy = Instantiate(obj, spawnPosition, Quaternion.identity);
                this.health = health;
                total_dummy_count++;
            }

        }
    }

    public void spawnSingleDummy(float health, float movementDistance)
    {

        Vector3 spawnPosition = spawnChecker.getRandomPosition(spawnCenter.position, 10f, -5f);
        dummy = Instantiate(obj, spawnPosition, Quaternion.identity);
        this.health = health;
        this.movementDistance = movementDistance;
        total_dummy_count++;
        
    }

    /*public void checkMovement()
    {
        if(moveDummies == true)
        {
            
        }
    }*/

    public void checkIfAlive()
    {
        if(total_dummy_count == 0)
        {
            switch (spawnType) {
                case SpawnType.MultipleDummy:
                    spawnMultipleDummies(100f);
                    break;
                case SpawnType.SingleDummy:
                    spawnSingleDummy(100f, movementDistance);
                    break;
            }
        }
    }
}
