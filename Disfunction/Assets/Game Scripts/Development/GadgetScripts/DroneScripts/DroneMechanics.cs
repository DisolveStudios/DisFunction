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
    public float upAcceleration;
    public float downAcceleration;
    public float deceleration;

    [Header("Adjust Camera Delay Effect")]
    public float rotationDelay = 5f;

    public Transform cameraTransform;

    private Vector3 targetCameraRotation;
    private float targetDroneYRotation;
    private float currentForwardSpeed = 0f;
    private float currentBackwardSpeed = 0f;
    private float currentUpSpeed = 0f;
    private float currentDownSpeed = 0f;
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

        targetCameraRotation = cameraTransform.localEulerAngles;
        targetDroneYRotation = transform.localEulerAngles.y;
    }

    private void Update()
    {
        checkClick();
        mouseMovement();

        // Handle movements
        upwardDownwardMovement();
        forwardBackwardMovement();
        sidewaysMovement();
    }

    private void mouseMovement()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        // Update target rotations
        targetCameraRotation.x -= mouseY * verticalCameraSensitivity * Time.deltaTime;
        targetDroneYRotation += mouseX * horizontalCameraSensitivity * Time.deltaTime;

        // Normalize angles for consistent behavior
        targetCameraRotation.x = NormalizeAngle(targetCameraRotation.x);
        targetDroneYRotation = NormalizeAngle(targetDroneYRotation);

        // Clamp the vertical rotation
        targetCameraRotation.x = Mathf.Clamp(targetCameraRotation.x, -4f, 35f);

        // Smoothly interpolate the camera's vertical rotation
        float smoothedVerticalRotation = Mathf.LerpAngle(cameraTransform.localEulerAngles.x, targetCameraRotation.x, Time.deltaTime * rotationDelay);

        // Smoothly interpolate the drone's horizontal rotation
        float smoothedDroneYRotation = Mathf.LerpAngle(transform.localEulerAngles.y, targetDroneYRotation, Time.deltaTime * rotationDelay);

        // Apply the smoothed rotations
        cameraTransform.localRotation = Quaternion.Euler(smoothedVerticalRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, smoothedDroneYRotation, 0f);
    }

    // Normalize angles to the range -180 to 180 degrees
    private float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
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
            currentBackwardSpeed += forwardAcceleration * Time.deltaTime; // Using the same acceleration variable
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
            currentRightSpeed = Mathf.Min(currentRightSpeed, topSpeed); // Clamp to top speed
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
            currentLeftSpeed = Mathf.Min(currentLeftSpeed, topSpeed); // Clamp to top speed
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
        // Upward movement with acceleration and deceleration
        if (moveUp)
        {
            currentUpSpeed += upAcceleration * Time.deltaTime;
            currentUpSpeed = Mathf.Min(currentUpSpeed, topSpeed); // Clamp to max speed
        }
        else
        {
            currentUpSpeed -= deceleration * Time.deltaTime;
            currentUpSpeed = Mathf.Max(currentUpSpeed, 0f); // Clamp to 0
        }

        if (currentUpSpeed > 0f)
        {
            transform.Translate(Vector3.up * currentUpSpeed * Time.deltaTime);
        }

        // Downward movement with acceleration and deceleration
        if (moveDown)
        {
            currentDownSpeed += downAcceleration * Time.deltaTime;
            currentDownSpeed = Mathf.Min(currentDownSpeed, topSpeed); // Clamp to max speed
        }
        else
        {
            currentDownSpeed -= deceleration * Time.deltaTime;
            currentDownSpeed = Mathf.Max(currentDownSpeed, 0f); // Clamp to 0
        }

        if (currentDownSpeed > 0f)
        {
            transform.Translate(Vector3.down * currentDownSpeed * Time.deltaTime);
        }
    }



}