using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    [SerializeField] BioTechSO rightBioTech = null;
    [SerializeField] BioTechSO leftBioTech = null;

    private Queue order = new Queue();
    private Queue DamageOrder = new Queue();

    void OnEnable()
    {
        //ArmamentClass.LaunchAction += Keep;
    }

    void OnDisable()
    {
        //ArmamentClass.LaunchAction -= Keep;
    }

    void Keep(int armamentDamage, int[] positions)
    {
        order.Enqueue(positions);
        DamageOrder.Enqueue(armamentDamage);
    }

    void Count(int armor, string LeftOrRight)
    {

    }

    public void DamageResult(int armor, string LeftOrRight)
    {
        if (LeftOrRight == "left")
        {
            foreach (int[] position in order.ToArray())
            {
                for (int i = 0; i < position.Length; i++)
                {
                    for (int j = 0; j < rightBioTech.currentPosition.Length; j++)
                    {
                        if (position[i] == rightBioTech.currentPosition[j])
                        {
                            Debug.Log("hit" + rightBioTech.currentPosition[j]);
                            foreach (int damage in DamageOrder.ToArray())
                            {
                                rightBioTech.currentHP = rightBioTech.currentHP - (damage - armor);
                                break;
                            }
                        }
                    }
                }

                if (order.Count != 0)
                {
                    order.Dequeue();
                    DamageOrder.Dequeue();
                }
            }

        }


        if (LeftOrRight == "right")
        {
            foreach (int[] position in order.ToArray())
            {
                for (int i = 0; i < position.Length; i++)
                {
                    for (int j = 0; j < leftBioTech.currentPosition.Length; j++)
                    {
                        if (position[i] == leftBioTech.currentPosition[j])
                        {
                            Debug.Log("hit" + leftBioTech.currentPosition[j]);
                            foreach (int damage in DamageOrder.ToArray())
                            {
                                rightBioTech.currentHP = leftBioTech.currentHP - (damage - armor);
                                break;
                            }
                        } 
                    }
                }

                if (order.Count != 0)
                {
                    order.Dequeue();
                    DamageOrder.Dequeue();
                }
            }
        }
    }
}
