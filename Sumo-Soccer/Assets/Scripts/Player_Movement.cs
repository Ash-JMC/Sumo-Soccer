using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public int playerNum;
    public float moveSpeed, rotateSpeed, boostSpeed, boostCD, baseMass;
    public Vector3 movement;

    private Rigidbody rb;
    private float nextBoost;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Move();
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

        if (Input.GetButton("Charge_" + playerNum) && Time.time >= nextBoost)
        {
            nextBoost = Time.time + boostCD;
            rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
        }
    }
    private void Move()
    {
        rb.AddForce(movement);

        if(movement.magnitude > 5f)
        {
            Quaternion targetRot = Quaternion.LookRotation(rb.velocity, Vector3.up);
            Quaternion lookRot = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
            transform.rotation = lookRot;
        }
        
    }
}
