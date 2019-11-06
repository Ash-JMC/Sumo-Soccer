using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public int playerNum;
    public float moveSpeed;
    public float rotateSpeed;
    public float baseMass;
    public Vector3 movement;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        Move();
    }


    private void GetInput()
    {
        float x = Input.GetAxis("X_" + playerNum);
        float z = Input.GetAxis("Z_" + playerNum);
        movement = Vector3.ClampMagnitude(new Vector3(x, 0, z) * moveSpeed, moveSpeed);
    }
    private void Move()
    {
        rb.velocity = movement;

        if(movement.magnitude > 1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(rb.velocity, Vector3.up);
            Quaternion lookRot = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
            transform.rotation = lookRot;
        }
        
    }
}
