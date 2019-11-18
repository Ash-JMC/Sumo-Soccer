using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ArenaMaster AM;
    public float interval, rotSpeed;
    public bool active = false;
    public float timer;
    public GameObject[] Pickups;
    public  int activePickup;
    private Player_Control PC;


    void Start()
    {
        timer = Time.time + interval;
    }
    

    
    void Update()
    {
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);

        if(!active && Time.time >= timer)
        {
            activePickup = Random.Range(0, Pickups.Length - 1);
            Pickups[activePickup].SetActive(true);
            timer = Time.time + interval;
            active = true;

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && active)
        {
            PC = col.gameObject.GetComponent<Player_Control>();
            Pickups[activePickup].SetActive(false);
            active = false;
            PickUp();
            timer = Time.time + interval;
        }

    }
    public void PickUp()
    {
        switch (activePickup)
        {
            case 3:
                break;
            case 2:
                break;
            case 1:
                break;
            case 0:
                StartCoroutine("Fatty");
                break;
            default:
                break;
        }
    }
    IEnumerator Fatty()
    {
        Rigidbody RB = PC.gameObject.GetComponent<Rigidbody>();
        PC.gameObject.transform.localScale *= 1.5f;
        PC.moveSpeed *= 2;
        RB.mass *= 3;
        RB.drag *= 0.5f;
        yield return new WaitForSeconds(5);
        PC.gameObject.transform.localScale /= 1.5f;
        PC.moveSpeed /= 2;
        RB.mass /= 3;
        RB.drag /= 0.5f;
    }

}
