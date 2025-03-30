using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float MoveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime ;
        float MoveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 Movement = new Vector3(MoveX, 0, MoveZ);
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        Player.Move(rotation * Movement);
    }
}
