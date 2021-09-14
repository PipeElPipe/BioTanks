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

    public int[] effectDuration = new int[25];
    public bool[] hidden = new bool[25];

    bool traped = false;
    int rooted = 0;

    bool keeping = false;
    bool right = false;
    bool left = false;

    string nombre;

    void OnEnable()
    {
        TurnSystem.EndPhaseAction += Reveal;
    }

    void OnDisable()
    {
        TurnSystem.EndPhaseAction -= Reveal;
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

                if (effectsPosition != 0)
                {
                    table.table[effectsPosition - 1].GetComponent<Renderer>().material.color = Color.green;
                    if (effectDuration[effectsPosition - 1] == 0)
                    {
                        effectDuration[effectsPosition - 1] = duration;
                    }
                }
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
                }
            }
        }
    }
}
