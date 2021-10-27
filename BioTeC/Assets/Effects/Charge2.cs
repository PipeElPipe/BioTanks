using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge2 : MonoBehaviour, IEffects2
{
    [SerializeField] Table table = null;
    [SerializeField] UITable UItable = null;
    [SerializeField] BioTechSO enemyBioTech = null;

    [SerializeField] int duration = 3;
    [SerializeField] int damage = 70;

    int[] effectDuration = new int[25];
    bool[] hidden = new bool[25];
    bool[] visible = new bool[25];

    bool keeping = false;
    bool noKeeping = false;

    bool erupted = false;

    [SerializeField]float wait = 0;

    void OnEnable()
    {
        TurnSystem.EndTurnAction += CountDown;

        TurnSystem.EndAttackAction += Normal;
        TurnSystem.EndDefenseAction += Normal;
        TurnSystem.EndTurnAction += Normal;

        TurnSystem.EndDefenseAction += Reveal;
        TurnSystem.EndTurnAction += Reveal;
        TurnSystem.EndAttackAction += Reveal;
    }

    void OnDisable()
    {
        TurnSystem.EndTurnAction -= CountDown;

        TurnSystem.EndAttackAction -= Normal;
        TurnSystem.EndDefenseAction -= Normal;
        TurnSystem.EndTurnAction -= Normal;

        TurnSystem.EndDefenseAction -= Reveal;
        TurnSystem.EndTurnAction -= Reveal;
        TurnSystem.EndAttackAction -= Reveal;
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
        switch (invisible)
        {
            case false:
                table.table[position - 1].GetComponent<Outline>().OutlineWidth = 10f;
                visible[position - 1] = true;

                if (effectsPosition != 0)
                {
                    table.table[effectsPosition - 1].GetComponent<Renderer>().material.color = Color.blue;
                    if (effectDuration[effectsPosition - 1] == 0)
                    {
                        effectDuration[effectsPosition - 1] = duration;
                    }
                }

                noKeeping = true;
                break;

            case true:
                hidden[position - 1] = true;

                if (effectsPosition != 0)
                {
                    if (effectDuration[effectsPosition - 1] == 0)
                    {
                        effectDuration[effectsPosition - 1] = duration;
                    }
                }

                keeping = true;
                break;
        }

    }

    void Normal()
    {
        if (noKeeping == true)
        {
            for (int i = 0; i < 25; i++)
            {
                if (visible[i] == true)
                {
                    table.table[i].GetComponent<Outline>().OutlineWidth = 0f;

                    visible[i] = false;
                }
            }
            noKeeping = false;
            StartCoroutine(Wait());
        }
    }

    void CountDown()
    {
        for (int i = 0; i < table.table.Length; i++)
        {
            if (effectDuration[i] > 0)
            {
                effectDuration[i]--;
            }

            if (effectDuration[i] == 1)
            {
                erupted = true;
            }
        }

        if (erupted == true)
        {
            StartCoroutine(Erupt());
        }
    }

    IEnumerator Erupt()
    {
        for (int i = 0; i < effectDuration.Length; i++)
        {
            if (effectDuration[i] == 1)
            {
                table.table[i].GetComponent<Renderer>().material.color = Color.cyan;
                UItable.UItable[i].GetComponent<Renderer>().material.color = Color.white;

                for (int j = 0; j < enemyBioTech.currentPosition.Length; j++)
                {
                    if (enemyBioTech.currentPosition[j] == i + 1)
                    {
                        //Debug.Log("blast " + enemyBioTech.currentPosition[j]);
                        enemyBioTech.currentHP = enemyBioTech.currentHP - damage;
                    }
                }
            }
        }
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < effectDuration.Length; i++)
        {
            if (effectDuration[i] == 1)
            {
                table.table[i].GetComponent<Renderer>().material.color = Color.white;
                effectDuration[i] = 0;
            }
        }
    }

    void Reveal()
    {
        if (keeping == true)
        {
            for (int i = 0; i < 25; i++)
            {
                if (hidden[i] == true)
                {
                    table.table[i].GetComponent<Outline>().OutlineWidth = 10f;

                    if (effectDuration[i] == duration)
                    {
                        table.table[i].GetComponent<Renderer>().material.color = Color.blue;
                    }

                    hidden[i] = false;
                }
            }
            keeping = false;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait);
        MarkOff();
    }

    void MarkOff()
    {
        for (int i = 0; i < effectDuration.Length; i++)
        {
            for (int j = 0; j < enemyBioTech.currentPosition.Length; j++)
            {
                if (effectDuration[i] == duration)
                {
                    if (i + 1 == enemyBioTech.currentPosition[j])
                    {
                        //Debug.Log("" + enemyBioTech.currentPosition[j] + " " + i);
                        effectDuration[i] = 0;
                    }
                }
            }
            if (effectDuration[i] == duration)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 0f;
            }

            if (effectDuration[i] == 0)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 0f;
                UItable.UItable[i].GetComponent<Renderer>().material.color = Color.gray;
                UItable.UItable[i].GetComponent<Outline>().OutlineWidth = 0f;
                table.table[i].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
