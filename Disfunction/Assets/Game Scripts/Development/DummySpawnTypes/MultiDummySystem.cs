using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiDummySystem : MonoBehaviour
{

    public GameObject obj;
    public GameObject dummy;
    public float health;
    public float numOfDummies;
    public float total_dummy_count;

    void Start()
    {
            spawnMultipleDummies(health);
    }

    private void Update()
    {
        checkIfAlive();
    }

    public void spawnMultipleDummies(float health)
    {
        for (int i = 0; i < numOfDummies; i++)
        {
            float x = Random.Range(-10f, 8.45f);
            float z = Random.Range(-12f, 1f);

            Vector3 spawnPosition = transform.position + new Vector3(x, -0.3f, z);
            dummy = Instantiate(obj, spawnPosition, Quaternion.identity);
            this.health = health;
            total_dummy_count++;
        }
    }

    public void checkIfAlive()
    {
        if(total_dummy_count == 0)
        {
            spawnMultipleDummies(100f);
        }
    }
}
