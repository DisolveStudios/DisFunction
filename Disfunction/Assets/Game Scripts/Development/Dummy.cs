using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public float health;
    public DamageSystem damageSystem;

    private void Start()
    {
        health = 100;
        damageSystem = GameObject.FindGameObjectWithTag("Respawn").GetComponent<DamageSystem>();
    }

    public void damage(float takeDamage)
    {
        health -= takeDamage;
        if (health <= 0)
        {
            Destroy(gameObject);
            onHealthZero();
        }
    }

    
    public void onHealthZero()
    {
        damageSystem.spawn(100f);
        Debug.Log("Cube respawned with full health!");
    }
    


    /*    private void OnDestroy()
        {
            if(damageSystem != null)
            {
                damageSystem.spawn(100);
            }
        }
    */
}
