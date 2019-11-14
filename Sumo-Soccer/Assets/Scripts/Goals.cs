using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public ArenaMaster AM;
    public bool leftTeam;
    public bool scored;
    public ParticleSystem confetti;
    public Color confettiColor;
    void Start()
    {
        if (leftTeam)
        {
            confettiColor = AM.colour_Left;
        }
        else
        {
            confettiColor = AM.colour_Right;
        }
        var confettiMain = confetti.main;
        confettiMain.startColor = confettiColor;
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Ball" && !scored)
        {
            AM.Scored(leftTeam);
            scored = true;
            confetti.Play();
        }
    }
}
