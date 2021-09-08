using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTableAr : MonoBehaviour
{
    private ArmamentClass armamentClass;

    void Start()
    {
        armamentClass = GetComponent<ArmamentClass>();
    }

    public void TurnOffOn(bool turn)
    {
        armamentClass.weaponEnable = 0;
        if (turn == true)
        {
            //armamentClass.enabled = !armamentClass.enabled;
            armamentClass.enabled = true;
        }
        else
        {
            armamentClass.enabled = false;
        }
    }
}
