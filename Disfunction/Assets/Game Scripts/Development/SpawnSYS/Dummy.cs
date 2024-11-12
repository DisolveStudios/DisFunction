using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public float health;
    public DummyDamageSystem dummyDamageSystem;
    public bool dummyMovement;
    public float movementDistance;
    public float moveSpeed = 5f;
    private Vector3 direction;

    public float minX;
    public float maxX;

    private void Start()
    {
        health = 100;
        dummyDamageSystem = GameObject.FindGameObjectWithTag("Respawn").GetComponent<DummyDamageSystem>();

        int random = Random.Range(0, 2);
        if (random == 0) direction = Vector3.right;
        else direction = Vector3.left;

        minX = transform.position.x - movementDistance;
        maxX = transform.position.x + movementDistance;
    }
    private void Update()
    {
        moveHorizontally();
    }

    public void moveHorizontally()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        

        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            direction = -direction;  
        }

        // Check for collision in the forward direction and change direction if needed
        if (Physics.Raycast(transform.position, direction, 1f))
        {
            direction = -direction;  // Reverse direction if about to collide
        }
    }

    public void damage(float takeDamage)
    {
        health -= takeDamage;
        if (health <= 0)
        {
            dummyDamageSystem.total_dummy_count--;
            Destroy(gameObject);
        }
    }
}
