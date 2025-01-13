using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class stickBombMechanics : MonoBehaviour
{
    private Impact impact;
    public GameObject bomb;
    public float closeRange;
    public float midRange;
    public float farRange;

    public float colliderExpansionSpeed;
    public float maxColliderSize;
    
    private RaycastHit hit;
    private Boolean isTriggered = false;
    public SphereCollider sphereCollider;

    public void triggerStickBomb()
    {
        isTriggered = true;
        sphereCollider.enabled = true;
    }

    void explode()
    {
        if (isTriggered && sphereCollider.radius < maxColliderSize)
        {
            sphereCollider.radius += colliderExpansionSpeed * Time.deltaTime;

            if (sphereCollider.radius >= maxColliderSize)
            {
                sphereCollider.radius = maxColliderSize;
                sphereCollider.enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = (collision.transform.position - bomb.transform.position).normalized;

        // Perform a raycast
        if (Physics.Raycast(bomb.transform.position, direction, out hit, Mathf.Infinity))
        {
            Debug.Log("Ray hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Ray did not hit any object.");
        }
    }

    void Update()
    {
        triggerStickBomb();
        explode();
    }
}
