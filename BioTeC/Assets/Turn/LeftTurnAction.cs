using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTurnAction : MonoBehaviour
{
    private Movement movement;
    [SerializeField] DamageCalculator2 dmg2 = null;
    //private Effects effects;

    void Start()
    {
        movement = GetComponent<Movement>();
        //effects = GetComponent<Effects>();
    }

    public void BioOn(bool turn)
    {
        if (turn == true)
        {
            movement.enabled = true;
            //effects.enabled = true;
        }
        else
        {
            movement.enabled = false;
            //effects.enabled = false;
        }
    }

    public void DmgOn(bool On)
    {
        if (On == true)
        {
            dmg2.enabled = true;
            //effects.enabled = true;
        }
        else
        {
            dmg2.enabled = false;
            //effects.enabled = false;
        }
    }
}
