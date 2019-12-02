using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public ArenaMaster AM;
    public float zoomStart, maxZoom, zoomSpeed;

    public GameObject[] players;
    public float[] playerDist;
    public float distDelta = 0;
    public Vector3 startPos, targetPos;

    void Start()
    {
        players = AM.Players;
        playerDist = new float[AM.playerCount];
        startPos = transform.position;
        targetPos = transform.position + (-transform.forward * distDelta);
    }

    void Update()
    {
        float bDist = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if (!players[i].GetComponent<Player_Control>().freeze)
            {
                float dist = Vector3.Distance(players[i].transform.position, AM.spawn_Balls.position);
                playerDist[i] = dist;
                if (dist > bDist)
                {
                    bDist = dist;
                }
            }
        }
        distDelta = Mathf.Clamp(bDist - zoomStart, 0, maxZoom);
        targetPos = startPos + (-transform.forward * distDelta);
        Vector3 lerpPos = Vector3.Lerp(transform.position, targetPos, zoomSpeed * Time.deltaTime);
        transform.position = lerpPos;


    }
}
