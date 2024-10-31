using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAround : MonoBehaviour
{
    public static float sensitivity = 500.0f;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        float MouseY = Input.GetAxis("Mouse Y");
            // float MouseY = Input.GetAxis("MouseY");
        transform.Rotate(-MouseY * sensitivity * Time.deltaTime, 0, 0);
    }
}
