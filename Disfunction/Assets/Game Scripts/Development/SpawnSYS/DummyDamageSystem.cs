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
    public float spawnRangeX;
    public float spawnRangeZ;
    public float health;
    public float numOfDummies;
    public float total_dummy_count;
    public Transform spawnCenter;
    public float spawnRadius = 1.5f;
    public bool moveDummies;
    public float movementRange;
    public float movementSpeed;

    private void Update()
    {
        checkIfAlive();
    }

    public void spawnMultipleDummies(float health)
    {
        total_dummy_count = 0;
        while (total_dummy_count < numOfDummies)
        {
            Vector3 spawnPosition = spawnChecker.getRandomPosition(spawnCenter.position, spawnRangeX, spawnRangeZ);

            
            dummy = Instantiate(obj, spawnPosition, Quaternion.identity);
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

    public void spawnSingleDummy(float health, float movementDistance)
    {

        Vector3 spawnPosition = spawnChecker.getRandomPosition(spawnCenter.position, 10f, -5f);
        dummy = Instantiate(obj, spawnPosition, Quaternion.identity);
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
