using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMechanics : MonoBehaviour
{

    public float moveSpeed;
    public float upSpeed;
    public float downSpeed;
    public float rotationSpeed;

    private bool moveForward;
    private bool moveBackward;
    private bool moveUp;
    private bool moveDown;
    private bool moveLeft;
    private bool moveRight;
    private bool rotateRight;
    private bool rotateLeft;


    private void Start()
    {
        
    }

    private void Update()
    {
        checkClick();
        move();
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

        if(moveDown)
        {
            transform.Translate(Vector3.down * downSpeed * Time.deltaTime);
        }

        if (moveForward)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (moveBackward)
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (moveRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (moveLeft)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
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