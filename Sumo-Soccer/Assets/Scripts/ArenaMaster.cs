using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaMaster : MonoBehaviour
{
    public int playerCount;
    public GameObject defaultPlayer, defaultBall, activeBall;
    public GameObject[] Players;

    public int score_Left, score_Right, roundCount;
    public Transform[] spawn_Players;
    public Transform spawn_Balls, agentSpace;
    public Color colour_Left, colour_Right;

    void Start()
    {
        Players = new GameObject[playerCount];
        SpawnActors(playerCount);
    }

    
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Reload(true);
        }
    }  

    public void Reload(bool newGame)
    {
        SceneManager.LoadScene("SS_Arena");
    }

    public void SpawnActors(int numPlayers)
    {
        
        for (int i = 0; i < numPlayers; i++)
        {
            Color playerCol;
            string team;
            if (i == 0 || i == 2)
            {
                playerCol = colour_Left;
                team = "Left";
            }
            else
            {
                playerCol = colour_Right;
                team = "Right";
            }
            GameObject newPlayer = Instantiate(defaultPlayer, spawn_Players[i].position, spawn_Players[i].rotation, agentSpace);
            Player_Control PC = newPlayer.GetComponent<Player_Control>();
            newPlayer.name = team+"Player_" + i;
            PC.body.material.color = playerCol;
            PC.playerNum = i + 1;
        }
        //LOAD BALL
        GameObject newBall = Instantiate(defaultBall, spawn_Balls.position, spawn_Balls.rotation, agentSpace);
        activeBall = newBall;


    }

    public void Scored(bool leftTeam)
    {
        if(leftTeam)
        {
            score_Left++;
            print(score_Left);
        }
        else
        {
            score_Right++;
            print(score_Right);
        }
    }
    
}
