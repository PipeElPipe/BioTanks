using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosiveEffect : MonoBehaviour, IEffects, IReturn
{
    IHeavy heavyInterface;

    [SerializeField] Movement movimientoLeft = null;
    [SerializeField] Movement1 movimientoRight = null;

    [SerializeField] Table table = null;
    [SerializeField] UITable UItable = null;
    [SerializeField] BioTechSO enemyBioTech = null;
    [SerializeField] int duration = 3;
    [SerializeField] int rootDuration = 2;

    public int[] effectDuration = new int[25];
    public bool[] hidden = new bool[25];

    bool traped = false;
    int rooted = 0;

    bool right = false;
    bool left = false;

    string nombre;


    void OnEnable()
    {
        TurnSystem.EndDefenseAction += MarkOff;
        TurnSystem.EndTurnAction += MarkOff;
        TurnSystem.EndTurnAction += CountDown;

        TurnSystem.EndDefenseAction += TrapedCheck;
        TurnSystem.EndTurnAction += TrapedCheck;
        TurnSystem.EndAttackAction += TrapedCheck;

        TurnSystem.EndDefenseAction += Reveal;
        TurnSystem.RevealAction += Reveal;

    }

    void OnDisable()
    {
        TurnSystem.EndTurnAction -= CountDown;
        TurnSystem.EndDefenseAction -= MarkOff;
        TurnSystem.EndTurnAction -= MarkOff;

        TurnSystem.EndDefenseAction -= TrapedCheck;
        TurnSystem.EndTurnAction -= TrapedCheck;
        TurnSystem.EndAttackAction -= TrapedCheck;

        TurnSystem.EndDefenseAction += Reveal;
        TurnSystem.RevealAction -= Reveal;
    }

    void Start()
    {
        heavyInterface = GetComponent<IHeavy>();

        nombre = transform.gameObject.name;
        if (nombre == "BioRight")
        {
            left = true;
        }

        if(nombre == "BioLeft")
        {
            right = true;
        }
    }

    void Update()
    {
        if (right == true)
        {
            if(rooted != 0)
            {
                movimientoRight.moveAble = false;
            }
            else
            {
                movimientoRight.moveAble = true;
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
            }
        }
    }

    public void Effect(int[] form, int[] effectPosition, bool invisible, string speed)
    {
        switch (speed)
        {
           case "Immediate":
              if (invisible == true)
              {
                    for (int j = 0; j < form.Length; j++)
                    {
                        UItable.UItable[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                        hidden[form[j] - 1] = true;
                    }
                    for (int i = 0; i < effectPosition.Length; i++)
                    {
                        int p = effectPosition[i] - 1;

                        UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.green;

                        if (effectDuration[form[p] - 1] == 0 || effectDuration[form[p] - 1] == duration + 2)
                        {
                            effectDuration[form[p] - 1] = duration;
                            hidden[form[p] - 1] = true;
                        }
                    }
              }
              else
              {
                    for (int j = 0; j < form.Length; j++)
                    {
                        table.table[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                        UItable.UItable[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                    }
                    for (int i = 0; i < effectPosition.Length; i++)
                    {
                        int p = effectPosition[i] - 1;

                        table.table[form[p] - 1].GetComponent<Renderer>().material.color = Color.green;
                        UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.green;

                        if (effectDuration[form[p] - 1] == 0 || effectDuration[form[p] - 1] == duration + 2)
                        {
                            effectDuration[form[p] - 1] = duration;
                        }
                    }
              }
            break;

            case "Heavy":
                heavyInterface.Recieve(form, invisible);

                for (int j = 0; j < form.Length; j++)
                {
                    UItable.UItable[form[j] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                }
                for (int i = 0; i < effectPosition.Length; i++)
                {
                    int p = effectPosition[i] - 1;

                    UItable.UItable[form[p] - 1].GetComponent<Renderer>().material.color = Color.green;

                    if (effectDuration[form[p] - 1] == 0)
                    {
                        effectDuration[form[p] - 1] = duration + 2;
                    }
                }
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

    void Reveal()
    {
        for (int i = 0; i < effectDuration.Length; i++)
        {
            if (effectDuration[i] == duration && hidden[i] == true)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 10f;
                table.table[i].GetComponent<Renderer>().material.color = Color.green;
                hidden[i] = false;
            }

            if (hidden[i] == true)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 10f;
                hidden[i] = false;
            }
        }
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
            if (effectDuration[i] == 0)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 0f;
                UItable.UItable[i].GetComponent<Renderer>().material.color = Color.gray;
                UItable.UItable[i].GetComponent<Outline>().OutlineWidth = 0f;
                table.table[i].GetComponent<Renderer>().material.color = Color.white;
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
            if (invisible == false)
            {
                if (effectDuration[i] == duration + 1 && result[i] == true)
                {
                    table.table[i].GetComponent<Renderer>().material.color = Color.green;
                    effectDuration[i] = duration;
                } 
            }

            if (invisible == true)
            {
                if (effectDuration[i] == duration && result[i] == true)
                {
                    table.table[i].GetComponent<Renderer>().material.color = Color.green;
                    effectDuration[i] = duration - 1;
                }
            }
        }

        if (invisible == true)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        MarkOff();
    }
}
