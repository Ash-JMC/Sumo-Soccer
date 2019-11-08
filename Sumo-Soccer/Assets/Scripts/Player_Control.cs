using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public int playerNum;
    public float moveSpeed, rotateSpeed, boostSpeed, boostCD, baseMass;
    public Vector3 movement;
    public MeshRenderer body;

    private Rigidbody rb;
    private float nextBoost, trailLife;
    private TrailRenderer tr;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        trailLife = 0f;

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
            trailLife = 0.3f;
        }
        if (trailLife > 0f)
        {
            tr.time = trailLife;
            trailLife = Mathf.Clamp(trailLife - Time.deltaTime * .75f, 0f, trailLife);
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
