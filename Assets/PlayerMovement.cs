using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float moveHor = speed*Time.deltaTime*horizontalInput;
        float moveVer = speed*Time.deltaTime*verticalInput;

        transform.position += new Vector3(moveHor, moveVer, 0f);
    }
}
