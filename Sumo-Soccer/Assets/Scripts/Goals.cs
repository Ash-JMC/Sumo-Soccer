using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{
    public ArenaMaster AM;
    public bool leftTeam;
    private bool scored;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Ball" && !scored)
        {
            AM.Scored(leftTeam);
            scored = true;
        }
    }
}
