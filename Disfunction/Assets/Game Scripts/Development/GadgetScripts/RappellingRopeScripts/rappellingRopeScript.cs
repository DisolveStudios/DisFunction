using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RappellingRopeScript : MonoBehaviour
{
    public float rappellingSpeed = 3f;
    private CharacterController characterController;
    private bool isRappelling = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isRappelling = !isRappelling; // Changing rappelling mode here 
        }

        if (isRappelling)
        {
            Vector3 rappelMovement = Vector3.down * rappellingSpeed * Time.deltaTime;
            characterController.Move(rappelMovement);
        }

        if (characterController.isGrounded)
        {
            isRappelling = false;
            Debug.Log("Player reached the ground, stopping rappel.");
        }

    }
}