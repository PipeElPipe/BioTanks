﻿using System.Collections;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject[] table = new GameObject[25];
    
    void OnEnable()
    {
        TurnSystem.EndDefenseAction += AimOff;
        TurnSystem.EndTurnAction += AimOff;
        TurnSystem.StartTurnAction += AimOff;
    }

    void OnDisable()
    {
        TurnSystem.EndDefenseAction -= AimOff;
        TurnSystem.EndTurnAction -= AimOff;
        TurnSystem.StartTurnAction -= AimOff;
    }

    void AimOff()
    {
        for (int i = 0; i < table.Length; i++)
        {
            table[i].GetComponent<Outline>().OutlineWidth = 0f;
            table[i].SetActive(true);                    
        }
    }
}