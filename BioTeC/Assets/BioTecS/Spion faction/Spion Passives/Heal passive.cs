using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healpassive : MonoBehaviour
{
    [SerializeField] BioTechSO enemyBioTech = null;
    [SerializeField] BioTechSO allyBioTech = null;

    void OnEnable()
    {
        DamageCalculator2.DamagedAction += Heal;
    }

    void OnDisable()
    {
        DamageCalculator2.DamagedAction -= Heal;
    }

    void Heal()
    {
        if (enemyBioTech.state == "sync")
        {
            allyBioTech.currentHP += 30;
        }
    }
}
