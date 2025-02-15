using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactBearer : MonoBehaviour
{
    public Impact impact;

    // Parent of the object.
    public Dummy parent;

    private void Start()
    {
        Transform parent = transform.parent;
        if(parent != null)
        {
            this.parent = parent.gameObject.GetComponent<Dummy>();
        }
    }
}
