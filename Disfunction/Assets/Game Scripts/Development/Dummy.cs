using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public float health;
    public MultiDummySystem multiDummySystem;

    private void Start()
    {
        health = 100;
        multiDummySystem = GameObject.FindGameObjectWithTag("Respawn").GetComponent<MultiDummySystem>();
    }

    public void damage(float takeDamage)
    {
        health -= takeDamage;
        if (health <= 0)
        {
            multiDummySystem.total_dummy_count--;
            Destroy(gameObject);
        }
    }
}
