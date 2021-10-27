using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synchro : MonoBehaviour
{
    [SerializeField] Table table = null;
    [SerializeField] UITable UItable = null;
    [SerializeField] BioTechSO enemyBioTech = null;

    bool[] effectLocation = new bool[25];

    void OnEnable()
    {
        TurnSystem.EndAttackAction -= ChekHit;
        TurnSystem.EndDefenseAction -= ChekHit;
        TurnSystem.EndTurnAction -= ChekHit;
    }

    void OnDisable()
    {
        TurnSystem.EndAttackAction -= ChekHit;
        TurnSystem.EndDefenseAction -= ChekHit;
        TurnSystem.EndTurnAction -= ChekHit;
    }

    public void UIEffect(int[] form, int[] effectPosition)
    {
        for (int j = 0; j < form.Length; j++)
        {
            UItable.UItable[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
        }

        for (int i = 0; i < effectPosition.Length; i++)
        {
            int p = effectPosition[i] - 1;
            UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void Effect(int position, int effectsPosition, bool invisible)
    {
        table.table[position - 1].GetComponent<Outline>().OutlineWidth = 10f;

        if (effectsPosition != 0)
        {
            table.table[effectsPosition - 1].GetComponent<Renderer>().material.color = Color.cyan;
        }

    }

    void ChekHit()
    {
        for (int i = 0; i < effectLocation.Length; i++)
        {
            for (int j = 0; j < enemyBioTech.currentPosition.Length; j++)
            {
                if (i + 1 == enemyBioTech.currentPosition[j])
                {
                    //Debug.Log("" + enemyBioTech.currentPosition[j] + " " + i);
                    enemyBioTech.state = "sync";
                }
            }
        }
    }
}
