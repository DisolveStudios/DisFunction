using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RappellingRopeScript : MonoBehaviour
{
    public float rappellingSpeed = 3f;
    public KeyCode keycodeForUp = KeyCode.Q;
    public KeyCode keycodeForDown = KeyCode.E;
    private CharacterController characterController;
    private bool isRappellingUp = false;
    private bool isRappellingDown = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        characterController = GetComponent<CharacterController>();
    }

    void RappelingDown()
    {
        if (Input.GetKeyDown(keycodeForDown) && !characterController.isGrounded)
        {
            isRappellingDown = !isRappellingDown; // Changing rappelling mode here 
            isRappellingUp = false;
        }

        if (isRappellingDown)
        {
            Vector3 rappelMovement = Vector3.down * rappellingSpeed * Time.deltaTime;
            characterController.Move(rappelMovement);
        }

        if (characterController.isGrounded)
        {
            isRappellingDown = false;
            Debug.Log("Player reached the ground, stopping rappel.");
        }
    }

    void RappelingUp()
    {
        if (Input.GetKeyDown(keycodeForUp))
        {
            isRappellingUp = !isRappellingUp; // Changing rappelling mode here 
            isRappellingDown = false;
        }

        if (isRappellingUp)
        {
            Vector3 rappelMovement = Vector3.up * rappellingSpeed * Time.deltaTime;
            characterController.Move(rappelMovement);
        }

        if (transform.position.y >= initialPosition.y)
        {
            isRappellingUp = false;
            Debug.Log("Player reached the top, stopping rappel.");
        }
    }

    void Update()
    {
        RappelingDown();
        RappelingUp();
    }
}