using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{

    public GameObject obj;
    public float health;

    public GameObject dummy;

    private void Start()
    {
        spawn(health);
    }

    public void spawn(float health)
    {
        float x = Random.Range(-3,5);

            Vector3 spawnPosition = transform.position + new Vector3(x, 0, 0);
            dummy = Instantiate(obj, spawnPosition, Quaternion.identity);
            this.health = health;
        
    }
}