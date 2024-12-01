using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DroneMechanics : MonoBehaviour
{

    public float moveSpeed;
    public float upSpeed;
    public float downSpeed;
    public float rotationSpeed;
    public float verticalCameraSensitivity;
    public float horizontalCameraSensitivity;
    public float forwardAcceleration;
    public float sideAcceleration;
    public float deceleration;

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
    private bool rotateRight;
    private bool rotateLeft;

    public Transform cameraTransform;

    private void Start()
    {
        
    }

    private void Update()
    {
        checkClick();
        move();
        cameraMovement();
    }

    private void cameraMovement()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        float verticalRotation = cameraTransform.localEulerAngles.x - mouseY * verticalCameraSensitivity * Time.deltaTime;

        float horizontalRotation = transform.localEulerAngles.y + mouseX * horizontalCameraSensitivity * Time.deltaTime;

        // Adjust for Unity's 0-360 degree rotation system
        if (verticalRotation > 180) verticalRotation -= 360;

        // Clamp the rotation between -25 and 25 degrees
        verticalRotation = Mathf.Clamp(verticalRotation, -25f, 35f);

        // Apply the clamped rotation
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, cameraTransform.localEulerAngles.z);

        transform.localRotation = Quaternion.Euler(transform.localEulerAngles.x, horizontalRotation, transform.localEulerAngles.z);
    }

    private void checkClick()
    {

        // *** Moving Forward 

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveForward = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveForward = false;
        }


        // *** Moving Backward 

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveBackward = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveBackward = false;
        }


        // Moving the drone Upwards

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveUp = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            moveUp = false;
        }

        // Moving the drone Downwards

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveDown = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveDown = false;
        }


        //  ***  Moving drone towards Right

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveRight = false;
        }


        //  ***  Moving drone towards Left

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            moveLeft = false;
        }


        //  ***  Rotating drone towards Right

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotateRight = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            rotateRight = false;
        }


        //  ***  Rotating drone towards Left

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotateLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            rotateLeft = false;
        }

    }

    private void move()
    {
        if (moveUp)
        {
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        }

        if (moveDown)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        }

        // Forward movement with acceleration and deceleration
        if (moveForward)
        {
            currentForwardSpeed += forwardAcceleration * Time.deltaTime;
            currentForwardSpeed = Mathf.Min(currentForwardSpeed, moveSpeed); // Clamp to max speed
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
            currentBackwardSpeed = Mathf.Min(currentBackwardSpeed, moveSpeed);
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

        // Right movement with acceleration and deceleration
        if (moveRight)
        {
            currentRightSpeed += sideAcceleration * Time.deltaTime;
            currentRightSpeed = Mathf.Min(currentRightSpeed, moveSpeed); // Clamp to max speed
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
            currentLeftSpeed = Mathf.Min(currentLeftSpeed, moveSpeed); // Clamp to max speed
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
        
        if (rotateRight)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (rotateLeft)
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }

    }

}