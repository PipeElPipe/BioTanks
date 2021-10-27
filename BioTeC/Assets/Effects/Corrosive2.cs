using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrosive2 : MonoBehaviour, IEffects2
{

    [SerializeField] Movement movimientoLeft = null;
    [SerializeField] Movement1 movimientoRight = null;

    [SerializeField] Table table = null;
    [SerializeField] UITable UItable = null;
    [SerializeField] BioTechSO enemyBioTech = null;
    [SerializeField] int duration = 3;
    [SerializeField] int rootDuration = 2;

    int[] effectDuration = new int[25];
    bool[] hidden = new bool[25];
    bool[] visible = new bool[25];

    bool traped = false;
    int rooted = 0;

    bool keeping = false;
    bool noKeeping = false;

    bool right = false;
    bool left = false;

    string nombre;

    [SerializeField] float wait = 0;

    void OnEnable()
    {
        TurnSystem.EndDefenseAction += TrapedCheck;
        TurnSystem.EndTurnAction += TrapedCheck;
        TurnSystem.EndAttackAction += TrapedCheck;

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
        TurnSystem.EndDefenseAction -= TrapedCheck;
        TurnSystem.EndTurnAction -= TrapedCheck;
        TurnSystem.EndAttackAction -= TrapedCheck;

        TurnSystem.EndTurnAction -= CountDown;

        TurnSystem.EndAttackAction -= Normal;
        TurnSystem.EndDefenseAction -= Normal;
        TurnSystem.EndTurnAction -= Normal;

        TurnSystem.EndDefenseAction -= Reveal;
        TurnSystem.EndTurnAction -= Reveal;
        TurnSystem.EndAttackAction -= Reveal;
    }

    void Start()
    {
        nombre = transform.gameObject.name;
        if (nombre == "BioRight")
        {
            left = true;
        }

        if (nombre == "BioLeft")
        {
            right = true;
        }
    }

    void Update()
    {
        if (right == true)
        {
            if (rooted != 0)
            {
                movimientoRight.moveAble = false;
            }
            else
            {
                movimientoRight.moveAble = true;
                enemyBioTech.state = null;
            }
        }
        else if (left == true)
        {
            if (rooted != 0)
            {
                movimientoLeft.moveAble = false;
            }
            else
            {
                movimientoLeft.moveAble = true;
                enemyBioTech.state = null;
            }
        }
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
            UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.green;
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
                    table.table[effectsPosition - 1].GetComponent<Renderer>().material.color = Color.green;
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

    void CountDown()
    {
        for (int i = 0; i < table.table.Length; i++)
        {
            if (effectDuration[i] > 0)
            {
                effectDuration[i]--;
            }

            if (effectDuration[i] == 0)
            {
                table.table[i].GetComponent<Renderer>().material.color = Color.white;
                UItable.UItable[i].GetComponent<Renderer>().material.color = Color.gray;
            }
        }

        if (traped == true)
        {
            rooted = rooted - 1;
        }
    }

    void TrapedCheck()
    {
        for (int i = 0; i < effectDuration.Length; i++)
        {
            if (effectDuration[i] < duration && effectDuration[i] != 0)
            {
                for (int j = 0; j < enemyBioTech.currentPosition.Length; j++)
                {
                    if (enemyBioTech.currentPosition[j] == i + 1)
                    {
                        traped = true;
                        enemyBioTech.state = "trapped";
                        effectDuration[i] = 0;

                        if (right == true)
                        {
                            rooted = rootDuration;
                        }
                        else if (left == true)
                        {
                            rooted = rootDuration;
                        }
                    }
                }
            }
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
                        table.table[i].GetComponent<Renderer>().material.color = Color.green;
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
            if(effectDuration[i] == duration)
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

