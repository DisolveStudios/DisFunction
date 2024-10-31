using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    Vector2 currentRotation;
    public static float mouseSensitivity = 1.0f;
    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, MouseX * mouseSensitivity, 0);
    }
}
