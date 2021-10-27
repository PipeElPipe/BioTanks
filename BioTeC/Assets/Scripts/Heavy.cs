using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : MonoBehaviour, IHeavy
{
    IReturn returnInterface;

    bool[] heavy = new bool[25];
    bool[] invHeavy = new bool[25];

    [SerializeField] bool enemyTurn;

    bool invLaunched = false;

    void Start()
    {
        returnInterface = GetComponent<IReturn>();

        TurnSystem.EndTurnAction += ReturnInvHeavy;
        TurnSystem.EndDefenseAction += ReturnHeavy;
        TurnSystem.EndTurnAction += Turn;
    }

    void OnDisable()
    {
        TurnSystem.EndTurnAction -= ReturnInvHeavy;
        TurnSystem.EndDefenseAction -= ReturnHeavy;
        TurnSystem.EndTurnAction -= Turn;
    }

    void Turn()
    {
        enemyTurn = !enemyTurn;
    }

    public void Recieve(int[] form, bool invisible)
    {
        if (invisible == false)
        {           
            for (int i = 0; i < form.Length; i++)
            {
                heavy[form[i] - 1] = true;
            }
        }
        else
        {
            invLaunched = true;

            for (int i = 0; i < form.Length; i++)
            {
                invHeavy[form[i] - 1] = true;
            }
        }
    }

    public void ReturnHeavy()
    {
        if (enemyTurn == true)
        {
            //returnInterface.Return(heavy, false);
        }
    }

    public void ReturnInvHeavy()
    {
        if (invLaunched == true && enemyTurn == true)
        {
            returnInterface.Return(invHeavy, true);
            invLaunched = false;
        }
    }

}
