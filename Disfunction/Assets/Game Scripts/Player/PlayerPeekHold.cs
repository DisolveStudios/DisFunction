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
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, degrees, ref r, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
