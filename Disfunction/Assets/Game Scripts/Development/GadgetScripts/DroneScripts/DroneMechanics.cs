using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DroneMechanics : MonoBehaviour
{

    [Header("Adjust Speed")]
    public float topSpeed;
    public float upSpeed;
    public float downSpeed;

    [Header("Adjust Sensitivity")]
    public float verticalCameraSensitivity;
    public float horizontalCameraSensitivity;

    [Header("Adjust Acceleration and Deceleration")]
    public float forwardAcceleration;
    public float sideAcceleration;
    public float deceleration;

    public Transform cameraTransform;

    private float currentForwardSpeed = 0f;
    private float currentBackwardSpeed = 0f;
    private float currentRightSpeed = 0f;
    private float currentLeftSpeed = 0f;

    private bool moveForward;
    private bool moveBackward;
    private bool moveUp;
    private bool moveDown;
    private bool moveLeft;
    private bool moveRight;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        checkClick();
        mouseMovement();

        // Handle movments
        upwardDownwardMovement();
        forwardBackwardMovement();
        sidewaysMovement();
    }

    private void mouseMovement()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        float verticalRotation = cameraTransform.localEulerAngles.x - mouseY * verticalCameraSensitivity * Time.deltaTime;

        float horizontalRotation = transform.localEulerAngles.y + mouseX * horizontalCameraSensitivity * Time.deltaTime;

        // Adjust for Unity's 0-360 degree rotation system
        if (verticalRotation > 180) verticalRotation -= 360;

        // Clamp the rotation between -25 and 25 degrees
        verticalRotation = Mathf.Clamp(verticalRotation, -4f, 35f);

        // Apply the clamped rotation
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, cameraTransform.localEulerAngles.y, cameraTransform.localEulerAngles.z);

        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, horizontalRotation, transform.localEulerAngles.z);
    }

    private void checkClick()
    {
        // Handle Forward and Backward Movement
        if (Input.GetKeyDown(KeyCode.W)) { moveForward = true; }
        if (Input.GetKeyUp(KeyCode.W)) { moveForward = false; }
        if (Input.GetKeyDown(KeyCode.S)) { moveBackward = true; }
        if (Input.GetKeyUp(KeyCode.S)) { moveBackward = false; }

        // Handle Vertical Movement
        if (Input.GetKeyDown(KeyCode.Space)) { moveUp = true; }
        if (Input.GetKeyUp(KeyCode.Space)) { moveUp = false; }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { moveDown = true; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { moveDown = false; }

        // Handle Horizontal Movement
        if (Input.GetKeyDown(KeyCode.D)) { moveRight = true; }
        if (Input.GetKeyUp(KeyCode.D)) { moveRight = false; }
        if (Input.GetKeyDown(KeyCode.A)) { moveLeft = true; }
        if (Input.GetKeyUp(KeyCode.A)) { moveLeft = false; }
    }

    private void forwardBackwardMovement() 
    {

        // Forward movement with acceleration and deceleration

        if (moveForward)
        {
            currentForwardSpeed += forwardAcceleration * Time.deltaTime;
            currentForwardSpeed = Mathf.Min(currentForwardSpeed, topSpeed); // Clamp to max speed
        }
        else
        {
            currentForwardSpeed -= deceleration * Time.deltaTime;
            currentForwardSpeed = Mathf.Max(currentForwardSpeed, 0f); // Clamp to 0
        }
        if (currentForwardSpeed > 0f)
        {
            transform.Translate(Vector3.forward * currentForwardSpeed * Time.deltaTime);
        }

        // Backward movement with acceleration and deceleration
        if (moveBackward)
        {
            currentBackwardSpeed += forwardAcceleration * Time.deltaTime; // Use the same acceleration variable
            currentBackwardSpeed = Mathf.Min(currentBackwardSpeed, topSpeed);
        }
        else
        {
            currentBackwardSpeed -= deceleration * Time.deltaTime;
            currentBackwardSpeed = Mathf.Max(currentBackwardSpeed, 0f);
        }
        if (currentBackwardSpeed > 0f)
        {
            transform.Translate(Vector3.back * currentBackwardSpeed * Time.deltaTime);
        }
    }
    private void sidewaysMovement() 
    {
        // Right movement with acceleration and deceleration
        if (moveRight)
        {
            currentRightSpeed += sideAcceleration * Time.deltaTime;
            currentRightSpeed = Mathf.Min(currentRightSpeed, topSpeed); // Clamp to max speed
        }
        else
        {
            currentRightSpeed -= deceleration * Time.deltaTime;
            currentRightSpeed = Mathf.Max(currentRightSpeed, 0f);
        }
        if (currentRightSpeed > 0f)
        {
            transform.Translate(Vector3.right * currentRightSpeed * Time.deltaTime);
        }

        // Left movement with acceleration and deceleration
        if (moveLeft)
        {
            currentLeftSpeed += sideAcceleration * Time.deltaTime;
            currentLeftSpeed = Mathf.Min(currentLeftSpeed, topSpeed); // Clamp to max speed
        }
        else
        {
            currentLeftSpeed -= deceleration * Time.deltaTime;
            currentLeftSpeed = Mathf.Max(currentLeftSpeed, 0f);
        }
        if (currentLeftSpeed > 0f)
        {
            transform.Translate(Vector3.left * currentLeftSpeed * Time.deltaTime);
        }
    }

    private void upwardDownwardMovement() 
    {
        if (moveUp)
        {
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }

        if (moveDown)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        }
    }


}