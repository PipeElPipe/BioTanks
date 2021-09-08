﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BioTech", menuName = "Add BioTech")]
public class BioTechSO : ScriptableObject
{

    public int MP;
    public int currentMP;
    public int maxHP;
    public int currentHP;
    public int Heat;
    public int currentHeat;
    public int armor;
    public string bioName;
    //string faction;
    public int[] position; //where the robot spawns
    public int[] currentPosition; //used for position updating

    //int[] Armament0_1; //?
    //int[] Armament0_2; //?

    void Start()
    {
        
    }

    void Update()
    {

    }
}