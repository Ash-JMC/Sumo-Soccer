using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ArenaMaster : MonoBehaviour
{
    public int playerCount;
    public GameObject defaultPlayer, defaultBall, activeBall;
    public GameObject[] Players;

    public int score_Left, score_Right, roundCount;
    public Transform[] spawn_Players;
    public Transform spawn_Balls, agentSpace;
    public Color colour_Left, colour_Right;
    public Text UI_score_Left, UI_score_Right, UI_Countdown;
    public Goals goal_Left, goal_Right;
    public GameObject pauseMenu;

    void Start()
    {
        Players = new GameObject[playerCount];
        UI_score_Left.color = colour_Left;
        UI_score_Right.color = colour_Right;
        SpawnActors(playerCount);
        StartCoroutine(Countdown());
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
        Time.timeScale = 1;
        SceneManager.LoadScene("SS_Arena");
    }
    public void NextRound()
    {
        print("Start next round");
        for(int i = 0; i < Players.Length; i++)
        {
            Players[i].transform.position = spawn_Players[i].position;
            Players[i].transform.rotation = spawn_Players[i].rotation;
            Players[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            Players[i].GetComponent<Player_Control>().freeze = true;
        }
        activeBall.transform.position = spawn_Balls.position;
        activeBall.transform.rotation = spawn_Balls.rotation;
        activeBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        activeBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        StartCoroutine(Countdown());
    }
    public void SpawnActors(int numPlayers)
    {
        
        for (int i = 0; i < numPlayers; i++)
        {
            Color playerCol;
            string team;
            bool leftTeam;
            if (i == 0 || i == 2)
            {
                playerCol = colour_Left;
                team = "Left";
                leftTeam = true;
            }
            else
            {
                playerCol = colour_Right;
                team = "Right";
                leftTeam = false;
            }
            GameObject newPlayer = Instantiate(defaultPlayer, spawn_Players[i].position, spawn_Players[i].rotation, agentSpace);
            Player_Control PC = newPlayer.GetComponent<Player_Control>();
            newPlayer.transform.localScale = new Vector3(1.25f, 1, 1.25f);
            newPlayer.name = team+"Player_" + i;
            PC.body.material.color = playerCol;
            PC.playerNum = i + 1;
            PC.freeze = true;
            PC.AM = GetComponent<ArenaMaster>();
            PC.leftTeam = leftTeam;
            Players[i] = newPlayer;
        }
        activeBall = Instantiate(defaultBall, spawn_Balls.position, spawn_Balls.rotation, agentSpace);
        activeBall.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void Scored(bool leftTeam)
    {
        if(leftTeam)
        {
            score_Left++;
            UI_score_Left.text = "SCORE:   " + score_Left;
        }
        else
        {
            score_Right++;
            UI_score_Right.text = "SCORE:   " + score_Right;
        }
        NextRound();
    }

    IEnumerator Countdown()
    {
        UI_Countdown.transform.parent.gameObject.SetActive(true);
        for (int timer = 3; timer > 0; timer--)
        {
            UI_Countdown.text = (timer).ToString();
            yield return new WaitForSeconds(1);
            UI_Countdown.text = (timer-1).ToString();
        }
        foreach (GameObject PC in Players)
        {
            PC.GetComponent<Player_Control>().freeze = false;
        }
        UI_Countdown.transform.parent.gameObject.SetActive(false);
        goal_Left.scored = false;
        goal_Right.scored = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
}
