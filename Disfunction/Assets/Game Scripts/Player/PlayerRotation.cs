using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float peekDegrees;

    public float peekTime;

    private float peekValue;

    private float r;

    Vector2 currentRotation;
    public static float mouseSensitivity = 1.0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            peekValue = -peekDegrees;
        } else if (Input.GetKey(KeyCode.Q))
        {
            peekValue = peekDegrees;
        } else
        {
            peekValue = 0;
        }

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, peekValue, ref r, peekTime);
        float MouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, MouseX * mouseSensitivity, 0);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, angle);
        
    }
}
