using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
             float MoveX = Input.GetAxis("Horizontal") * 10f * Time.deltaTime ;
            float MoveZ = Input.GetAxis("Vertical") * 10f  * Time.deltaTime;

            Vector3 Movement = new Vector3(MoveX, 0, MoveZ);
            Player.Move(transform.rotation * Movement);
    }
}
