using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPeekHold : MonoBehaviour
{
    float r;
    public float degrees;

    float peek;

    public GunMechanics fpsCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            peek = -degrees;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            peek = degrees;
        }
        else
        {
            peek = 0;
        }

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, peek, ref r, 0.1f);

        fpsCam.peekDegree = Quaternion.Euler(0,0,angle);
    }
}
