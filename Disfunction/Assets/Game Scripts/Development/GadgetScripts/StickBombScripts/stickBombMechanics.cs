using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickBombMechanics : MonoBehaviour
{
    public GameObject bomb;

    public Distance extremeClose;
    public Distance close;
    public Distance mildlyClose;

    public float colliderExpansionSpeed;
    public float maxColliderSize;

    public float DamagePowerForExtremeCloseRange;
    public float DamagePowerForCloseRange;
    public float DamagePowerForMildlyCloseRange;

    public Dummy dummy;

    public SphereCollider sphereCollider;

    private RaycastHit hit;
    private Boolean isTriggered = false;

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

        Unwrap distance = new Unwrap(6, 0);
        distance.extremeClose = 1.25f;
        distance.close = 2f;
        distance.mildlyClose = 4f;

        if (Physics.Raycast(bomb.transform.position, direction, out hit, Mathf.Infinity))
        {
            ImpactBearer impactBearer = hit.transform.GetComponent<ImpactBearer>();

            if (impactBearer != null)
            {
                float impactDistance = Geometry.GetDistance(transform.position, hit.transform.position);

                float damage = 0;
                if (impactDistance <= distance.distance(extremeClose))
                {
                    damage = DamagePowerForExtremeCloseRange;
                }
                else if (impactDistance > distance.distance(extremeClose) && impactDistance <= distance.distance(close))
                {
                    damage = DamagePowerForCloseRange;
                }
                else if (impactDistance > distance.distance(close) && impactDistance <= distance.distance(mildlyClose))
                {
                    damage = DamagePowerForMildlyCloseRange;
                }

                impactBearer.parent.damage(damage, impactBearer.impact);

            }
        }
    }

    private void Start()
    {
        triggerStickBomb();
    }

    void Update()
    {
        explode();
    }
}
