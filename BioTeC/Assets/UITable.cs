using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITable : MonoBehaviour
{
    public GameObject[] UItable = new GameObject[25];
    [SerializeField] GameObject ThisTable = null;
    [SerializeField] ArmamentClass weaponMode = null;
    [SerializeField] GameObject Boxes = null;

    // Start is called before the first frame update
    void OnEnable()
    {
        TurnSystem.EndAttackAction += TurnOff;
        TurnSystem.EndTurnAction += TurnOff;
    }

    void OnDisable()
    {
        TurnSystem.EndAttackAction -= TurnOff;
        TurnSystem.EndTurnAction -= TurnOff;
    }

    void Update()
    {
        if (weaponMode.weaponEnable == 1)
        {
            Boxes.SetActive(true);
        }
        else if (weaponMode.weaponEnable == 0)
        {
            Boxes.SetActive(false);
        }
    }

    void TurnOff()
    {
        for(int i = 0; i < UItable.Length; i++)
        {
            UItable[i].GetComponent<Outline>().OutlineWidth = 0f;
        }
        ThisTable.SetActive(false);
    }
}

