using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Impact
{
    Head,
    Body,
    Foot
}

public class Dummy : MonoBehaviour
{
    public float health;
    public float movementSpeed;
    public float movementRange;
    public float minX;
    public float maxX;
    public float bodyDamageRatio;
    public float footDamageRatio;

    public int damageRatio;

    public DummyDamageSystem dummyDamageSystem;
    public GameObject head;
    public GameObject body;
    public GameObject leg;

    private Vector3 direction;

    private void Start()
    {
        health = 100;
        GameObject spawner = GameObject.FindGameObjectWithTag("Respawn");
        
        if(spawner != null)
        {
            dummyDamageSystem = spawner.GetComponent<DummyDamageSystem>();
        }

        int random = Random.Range(0, 2);
        if (random == 0) direction = Vector3.right;
        else direction = Vector3.left;
        minX = transform.position.x - movementRange;
        maxX = transform.position.x + movementRange;
    }

    private void Update()
    {
        moveHorizontally();
    }

    public void moveHorizontally()
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        if (transform.position.x <= minX || transform.position.x >= maxX || Physics.Raycast(transform.position, direction, 1f))
        {
            direction = -direction;  
        }

    }

    public void damage(float takeDamage, Impact impact)
    {
        switch (impact)
        {
            case Impact.Head:
                health -= takeDamage * 1/1;
                break;

            case Impact.Body:
                health -= takeDamage * bodyDamageRatio;
                break;

            case Impact.Foot:
                health -= takeDamage * footDamageRatio;
                break;
        }
        if(health <= 0)
        {
            if (dummyDamageSystem != null)
            {
                dummyDamageSystem.total_dummy_count--;
            }
            Destroy(gameObject);
        }
    }
}
