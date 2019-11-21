﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public int playerNum;
    public float moveSpeed, rotateSpeed, boostSpeed, boostCD, baseMass;
    public Vector3 movement;
    public Renderer body;
    public ParticleSystem sweatyCD;
    public GameObject impactFX;
    public Animator AC;

    [HideInInspector]
    public bool freeze = true, leftTeam, charging = false;
    [HideInInspector]
    public ArenaMaster AM;

    private Rigidbody rb;
    private float nextBoost, trailLife;
    private TrailRenderer tr;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        ParticleSystem.MainModule psMain = sweatyCD.main;
        psMain.duration = boostCD * 0.75f;
        trailLife = 0f;
        if(playerNum == 3 || playerNum == 4)
        {
            Color newCol = body.material.color;
            newCol.r += 0.25f;
            newCol.g += 0.25f;
            newCol.b += 0.25f;
            body.material.color = newCol;
        }
        
    }

    void Update()
    {
        if(!freeze) { GetInput();}
        else AC.SetFloat("Speed", 0);


    }

    void FixedUpdate()
    {
        if (!freeze) { Move(); }
    }


    private void GetInput()
    {
        float x = Input.GetAxis("X_" + playerNum);
        float z = Input.GetAxis("Z_" + playerNum);
        movement = Vector3.ClampMagnitude(new Vector3(x, 0, z) * moveSpeed, moveSpeed);


        if (Input.GetButton("Charge_" + playerNum) && Time.time >= nextBoost)
        {
            impactFX.SetActive(false);
            nextBoost = Time.time + boostCD;
            rb.AddForce(transform.forward * (boostSpeed * (rb.mass / 2)), ForceMode.Impulse);
            trailLife = 0.3f;
            sweatyCD.gameObject.SetActive(true);
            charging = true;
            AC.SetTrigger("Charge");
           // AC.ResetTrigger("Charge");
        }
        if (trailLife > 0f)
        {
            tr.time = trailLife;
            trailLife = Mathf.Clamp(trailLife - Time.deltaTime * .75f, 0f, trailLife);
            
        }
        else if (Time.time >= nextBoost && sweatyCD.gameObject.activeSelf)
        {
            sweatyCD.gameObject.SetActive(false);
            charging = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            AM.Pause();
        }
        AC.SetFloat("Speed", movement.magnitude);

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
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "OutZone")
        {
            AM.Scored(!leftTeam);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Player")
        {
            Player_Control PC = col.gameObject.GetComponent<Player_Control>();
            if (PC.leftTeam != leftTeam)
            {
                if(charging && !PC.charging)
                {
                    //Is this fun?
                    float vel = rb.velocity.magnitude*(rb.mass/2);
                    charging = false;
                    Rigidbody enemyRB = col.collider.gameObject.GetComponent<Rigidbody>();
                    enemyRB.AddForce(transform.forward * vel, ForceMode.Impulse);
                    impactFX.SetActive(true);
                    PC.Slammed();
                }
            }
            else
            {
                print("Hit a bro!");
            }
        }
    }
    public void Slammed()
    {
        AC.SetTrigger("Slammed");
        //AC.ResetTrigger("Slammed");
    }
}
