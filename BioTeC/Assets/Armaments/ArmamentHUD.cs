using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmamentHUD : MonoBehaviour
{
    [SerializeField] Text mode = null;
    [SerializeField] ArmamentClass armamentClass = null;
    [SerializeField] Text[] code =  new Text[6];

    void OnEnable()
    {
        armamentClass.Show(0);
        for (int i = 0; i < 6; i++)
        {
            code[i].text = armamentClass.armamentArr[i].armamentName;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            armamentClass.Show(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            armamentClass.Show(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            armamentClass.Show(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            armamentClass.Show(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            armamentClass.Show(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            armamentClass.Show(5);
        }
        if(armamentClass.weaponEnable == 0)
        {
            mode.text = "Weapons: Off";
        }
        if (armamentClass.weaponEnable == 1)
        {
            mode.text = "Weapons: On";
        }
    }
}
