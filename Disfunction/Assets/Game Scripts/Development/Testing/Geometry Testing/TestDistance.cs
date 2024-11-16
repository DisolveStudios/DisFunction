using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistance : MonoBehaviour
{
    public Transform target;
    public float distance;

    // Update is called once per frame
    void Update()
    {
        distance = Geometry.GetDistance(transform.position, target.position);
        Debug.Log(distance);
    }
}
