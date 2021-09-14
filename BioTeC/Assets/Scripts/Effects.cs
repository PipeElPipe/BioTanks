using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] Table table = null;
    [SerializeField] UITable UItable = null;

    IEffects effectsInterface;
    IHeavy heavyInterface;

    bool[] normal = new bool[25];

    public delegate void InvisibleEvent();
    public static event InvisibleEvent InvisibleAction;

    void OnEnable()
    {   
        TurnSystem.EndDefenseAction += MarkOff;
        TurnSystem.RevealAction += Reveal;

        effectsInterface = GetComponent<IEffects>();
        heavyInterface = GetComponent<IHeavy>();
    }

    void OnDisable()
    {
        TurnSystem.EndDefenseAction -= MarkOff;
        TurnSystem.RevealAction -= Reveal;
    }

    public void NullEffect(int[] form, bool invisible, string speed)
    {
        if (speed == "Immediate")
        {
            Normal(form, invisible, speed);
        }
        if (speed == "Heavy")
        {
            heavyInterface.Recieve(form, invisible);

            for (int i = 0; i < form.Length; i++)
            {
                UItable.UItable[form[i] - 1].GetComponent<Outline>().OutlineWidth = 10f;
            }
        }
    }

    public void Effect(int[] form, int[] effectPosition, bool invisible, string speed)
    {
        effectsInterface.Effect(form, effectPosition, invisible, speed);
    }

    public void Normal(int[] form, bool invisible, string speed)
    {
        if (invisible == true)
        {
            if (InvisibleAction != null)
            {
                InvisibleAction();
            }
            for (int i = 0; i < form.Length; i++)
            {
                UItable.UItable[form[i] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                normal[form[i] - 1] = true;
            }
        }
        else
        {
            for (int i = 0; i < form.Length; i++)
            {
                //table.table[form[i] - 1].GetComponent<Renderer>().material.color = Color.black;
                table.table[form[i] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                //UItable.UItable[form[i] - 1].GetComponent<Renderer>().material.color = Color.black;
                UItable.UItable[form[i] - 1].GetComponent<Outline>().OutlineWidth = 10f;
                normal[form[i] - 1] = true;
            }
        }
    }

    public void Reveal()
    {
        for (int i = 0; i < normal.Length; i++)
        {
            if (normal[i] == true)
            {
                table.table[i].GetComponent<Outline>().OutlineWidth = 10f;
                //normal[i] = false;
            }
        }
    }

    public void MarkOff()
    {
        for (int i = 0; i < normal.Length; i++)
        {
            if (normal[i] == true)
            {
                //table.table[i].GetComponent<Renderer>().material.color = Color.white;
                //UItable.UItable[i].GetComponent<Renderer>().material.color = Color.white;
                table.table[i].GetComponent<Outline>().OutlineWidth = 0f;
                UItable.UItable[i].GetComponent<Outline>().OutlineWidth = 0f;
                normal[i] = false;
            }
        }
    }



    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        MarkOff();
    }
}
