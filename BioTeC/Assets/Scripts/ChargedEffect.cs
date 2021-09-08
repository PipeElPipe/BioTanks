using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedEffect : MonoBehaviour, IEffects, IReturn
{
    [SerializeField] Table table = null;
    [SerializeField] UITable UItable = null;
    [SerializeField] int damage = 70;
    [SerializeField] BioTechSO enemyBioTech = null;
    [SerializeField] int duration = 3;

    int[] effectDuration = new int[25];
    bool[] hidden = new bool[25];

    bool erupted = false;

    IHeavy heavyInterface;

    void OnEnable()
    {
        TurnSystem.EndTurnAction += CountDown;
        TurnSystem.EndDefenseAction += MarkOff;

        //for heavy
        TurnSystem.EndAttackAction += MarkOff;

        TurnSystem.RevealAction += Reveal;

        heavyInterface = GetComponent<IHeavy>();
    }

    void OnDisable()
    {
        TurnSystem.EndTurnAction -= CountDown;
        TurnSystem.EndDefenseAction -= MarkOff;

        //for heavy
        TurnSystem.EndAttackAction -= MarkOff;

        TurnSystem.RevealAction -= Reveal;
    }

    public void Effect(int[] form, int[] effectPosition, bool invisible, string speed)
    {
        if (speed == "Immediate")
        {
            Charge(form, effectPosition, invisible); 
        }
        if(speed == "Heavy")
        {
            heavyInterface.Recieve(form, invisible);

            for (int j = 0; j < form.Length; j++)
            {
                UItable.UItable[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
            }

            for (int i = 0; i < effectPosition.Length; i++)
            {
                int p = effectPosition[i] - 1;

                UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.blue;

                if (effectDuration[form[p] - 1] == 0)
                {
                    effectDuration[form[p] - 1] = duration + 3;
                }
            }
        }
    }
    
    void Charge(int[] form, int[] effectPosition, bool invisible)
    {
        if(invisible == true)
        {
            for (int j = 0; j < form.Length; j++)
            {
                UItable.UItable[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                hidden[form[j] - 1] = true;
            }
            for (int i = 0; i < effectPosition.Length; i++)
            {
                int p = effectPosition[i] - 1;

                UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.blue;

                if (effectDuration[form[p] - 1] == 0 || effectDuration[form[p] - 1] == duration + 3)
                {
                    effectDuration[form[p] - 1] = duration + 1;
                    hidden[form[p] - 1] = true;
                }
            }
        }
        else
        {
            for (int j = 0; j < form.Length; j++)
            {
                //table.table[form[j] - 1].GetComponent<Renderer>().material.color = Color.black;
                table.table[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                //UItable.UItable[form[j] - 1].GetComponent<Renderer>().material.color = Color.black;
                UItable.UItable[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
            }
            for (int i = 0; i < effectPosition.Length; i++)
            {
                int p = effectPosition[i] - 1;

                table.table[form[p] - 1].GetComponent<Renderer>().material.color = Color.blue;
                UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.blue;

                if (effectDuration[form[p] - 1] == 0 || effectDuration[form[p] - 1] == duration + 3)
                {
                    effectDuration[form[p] - 1] = duration + 1;
                }
            }
        }
    }

    void CountDown()
    {
        for (int i = 0; i < table.table.Length; i++)
        {
            if (effectDuration[i] > 0)
            {
                effectDuration[i]--;
                if (effectDuration[i] == 1)
                {
                    erupted = true;
                }
            }
        }

        if(erupted == true)
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
            }
        }
    }

    void MarkOff()
    {
        for (int i = 0; i < effectDuration.Length; i++)
        {
            for (int j = 0; j < enemyBioTech.currentPosition.Length; j++)
            {
                if (effectDuration[i] == duration + 1)
                {
                    if (i + 1 == enemyBioTech.currentPosition[j])
                    {
                        //Debug.Log("" + enemyBioTech.currentPosition[j] + " " + i);
                        effectDuration[i] = 0;
                    } 
                }
            }
            if (effectDuration[i] == 0)
            {
                table.table[i].GetComponent<Renderer>().material.color = Color.white;
                table.table[i].GetComponent<Outline>().OutlineWidth = 0f;
                UItable.UItable[i].GetComponent<Renderer>().material.color = Color.gray;
                UItable.UItable[i].GetComponent<Outline>().OutlineWidth = 0f;
            }
        }
    }

    void Reveal()
    {
        for (int i = 0; i < UItable.UItable.Length; i++)
        {
            if (hidden[i] == true && effectDuration[i] == duration + 1)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 10f;
                table.table[i].GetComponent<Renderer>().material.color = Color.blue;
                hidden[i] = false;
            }

            if (hidden[i] == true)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 10f;
                hidden[i] = false;
            }
        }
    }

    public void Return(bool[] result, bool invisible)
    {
        for (int j = 0; j < result.Length; j++)
        {
            if (result[j] == true)
            {
                table.table[j].GetComponent<Outline>().OutlineWidth = 10f; 
            }
        }
        for (int i = 0; i < effectDuration.Length; i++)
        {
            if (effectDuration[i] == duration + 2 && result[i] == true)
            {
                table.table[i].GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}
