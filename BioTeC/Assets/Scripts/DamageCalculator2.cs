using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculator2 : MonoBehaviour
{
    [SerializeField] BioTechSO enemyBioTech = null;

    public delegate void EnemyDamagedEvent();
    public static event EnemyDamagedEvent DamagedAction;

    private Queue order = new Queue();
    private Queue DamageOrder = new Queue();

    private Queue HeavyOrder = new Queue();
    private Queue HeavyDamageOrder = new Queue();

    private Queue ReactiveOrder = new Queue();
    private Queue ReactiveDamageOrder = new Queue();

    int[] delay = new int [3];

    int coordinate = 0;


    void OnEnable()
    {
        ArmamentClass.LaunchAction += Keep;
        TurnSystem.EndPhaseAction += Count;
    }

    void OnDisable()
    {
        ArmamentClass.LaunchAction -= Keep;
        TurnSystem.EndPhaseAction -= Count;
    }

    void Keep(int armamentDamage, int[] positions, string speed)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (speed == "Reactive")
            {
                ReactiveOrder.Enqueue(positions[i]);
                coordinate = coordinate + 1;
            }
            if (speed == "Immediate")
            {
                order.Enqueue(positions[i]);
                coordinate = coordinate + 1;
            }
            if (speed == "Heavy")
            {
                HeavyOrder.Enqueue(positions[i]);
                coordinate = coordinate + 1;
            }
        }

        switch (speed)
        {
            case "Reactive":
                //ReactiveOrder.Enqueue(positions);
                ReactiveDamageOrder.Enqueue(armamentDamage);

                for (int i = 0; i < coordinate; i++)
                {
                    ReactiveDamageOrder.Enqueue(armamentDamage);
                    if (i == coordinate - 1)
                    {
                        coordinate = 0;
                    }
                }

                delay[0] = 4;
                break;

            case "Immediate":
                //order.Enqueue(positions);
                for (int i = 0; i < coordinate; i++)
                {
                    DamageOrder.Enqueue(armamentDamage);
                    if (i == coordinate - 1)
                    {
                        coordinate = 0;
                    }
                }

                delay[1] = 2;
                break;

            case "Heavy":
                //HeavyOrder.Enqueue(positions);
                HeavyDamageOrder.Enqueue(armamentDamage);

                for (int i = 0; i < coordinate; i++)
                {
                    HeavyDamageOrder.Enqueue(armamentDamage);
                    if (i == coordinate - 1)
                    {
                        coordinate = 0;
                    }
                }

                delay[2] = 6;
                break;
        }
    }

    void Count()
    {
        for (int i = 0; i < delay.Length; i++)
        {
            delay[i] = delay[i] - 1;
        }

        if(delay[1] == 0)
        {
            DamageResult();
        }


        if (delay[0] == 0)
        {
            foreach (int position in ReactiveOrder.ToArray())
            {
                order.Enqueue(position);

                if (ReactiveOrder.Count > 0)
                {
                    ReactiveOrder.Dequeue();
                }
            }
            foreach (int attack in ReactiveDamageOrder.ToArray())
            {
                DamageOrder.Enqueue(attack);

                if (ReactiveDamageOrder.Count > 0)
                {
                    ReactiveDamageOrder.Dequeue();
                }
            }
            DamageResult();
        }

        if (delay[2] == 0)
        {
            foreach (int position in HeavyOrder.ToArray())
            {
                order.Enqueue(position);

                if (HeavyOrder.Count > 0)
                {
                    HeavyOrder.Dequeue();
                }
            }
            foreach (int attack in HeavyDamageOrder.ToArray())
            {
                DamageOrder.Enqueue(attack);

                if (HeavyDamageOrder.Count > 0)
                {
                    HeavyDamageOrder.Dequeue();
                }
            }
            DamageResult();
        }
    }

    public void DamageResult()
    {
        if (order.Count > 0)
        {
            foreach (int position in order.ToArray())
            {
                for (int j = 0; j < enemyBioTech.currentPosition.Length; j++)
                {
                    if (position == enemyBioTech.currentPosition[j])
                    {

                        if (DamagedAction != null)
                        {
                            DamagedAction();
                        }

                        //Debug.Log("hit" + enemyBioTech.currentPosition[j]);
                        foreach (int damage in DamageOrder.ToArray())
                        {
                            enemyBioTech.currentHP = enemyBioTech.currentHP - (damage - enemyBioTech.armor);
                            break;
                        }
                    }
                }

                if (order.Count > 0)
                {
                    order.Dequeue();
                    DamageOrder.Dequeue();
                }
            }
        }
    }
}
