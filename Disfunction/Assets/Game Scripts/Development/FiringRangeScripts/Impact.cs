using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    public DummyImpact dummyImpact;

    // Parent of the object.
    public Dummy parent;

    private void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Dummy>();
    }
}
