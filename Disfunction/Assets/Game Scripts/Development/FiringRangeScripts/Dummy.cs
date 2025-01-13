using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DummyImpact
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
    public float minMovementX;
    public float maxMovementX;
    public float bodyDamageRatio;
    public float footDamageRatio;

    public int damageRatio;

    public DummySpawnSystem dummyDamageSystem;
    public GameObject head;
    public GameObject body;
    public GameObject leg;

    private Vector3 direction;

    private void Start()
    {
        GameObject spawner = GameObject.FindGameObjectWithTag("Respawn");
        
        if(spawner != null)
        {
            dummyDamageSystem = spawner.GetComponent<DummySpawnSystem>();
        }

        int random = Random.Range(0, 2);
        if (random == 0) direction = Vector3.right;
        else direction = Vector3.left;
        minMovementX = transform.position.x - movementRange;
        maxMovementX = transform.position.x + movementRange;
    }

    private void Update()
    {
        moveHorizontally();
    }

    public void moveHorizontally()
    {
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        if (transform.position.x <= minMovementX || transform.position.x >= maxMovementX || Physics.Raycast(transform.position, direction, 1f))
        {
            direction = -direction;  
        }

    }

    public void damage(float takeDamage, DummyImpact impact)
    {
        switch (impact)
        {
            case DummyImpact.Head:
                health -= takeDamage * 1/1;
                break;

            case DummyImpact.Body:
                health -= takeDamage * bodyDamageRatio;
                break;

            case DummyImpact.Foot:
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
