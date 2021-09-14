using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] CombatHUD combat = null;

    [SerializeField] BioTechSO rightBioTech = null;
    [SerializeField] BioTechSO leftBioTech = null;

    [SerializeField] ArmamentClass rightArmament = null;
    [SerializeField] ArmamentClass leftArmament = null;

    [SerializeField] LeftTurnAction leftAction = null;
    [SerializeField] RightTurnAction rightAction = null;

    [SerializeField] LeftTableAr leftTable = null;
    [SerializeField] RightTableAr rightTable = null;

    [SerializeField] GameObject leftUITable = null;
    [SerializeField] GameObject rightUITable = null;

    //[SerializeField] GameObject armGroup = null;
    [SerializeField] CameraManager cameras = null;


    bool invisibleShot = false;

    public int actualTurn = 1;
    public string phase;

    public delegate void EndPhaseEvent();
    public static event EndPhaseEvent EndPhaseAction;

    public delegate void StartTurnEvent();
    public static event StartTurnEvent StartTurnAction;

    public delegate void EndTurn();   
    public static event EndTurn EndTurnAction;

    public delegate void EndDefense();
    public static event EndDefense EndDefenseAction;

    public delegate void EndAttack();
    public static event EndAttack EndAttackAction;

    public delegate void Reveal();
    public static event Reveal RevealAction;

    void Start()
    {
        rightBioTech.currentHP = rightBioTech.maxHP;
        leftBioTech.currentHP = leftBioTech.maxHP;
        StartCoroutine(Provision());
    }

    void OnEnable()
    {
        Effects.InvisibleAction += InvisibleTrue;
    }

    void OnDisable()
    {
        Effects.InvisibleAction -= InvisibleTrue;
    }

    void InvisibleTrue()
    {
        invisibleShot = true;
    }

    IEnumerator Provision()
    {
        phase = "Provision";
        if (actualTurn%2 != 0)
        {
            leftBioTech.currentHeat = leftBioTech.Heat;
            leftBioTech.currentMP = leftBioTech.MP;
            for (int i = 0; i < rightArmament.armamentArr.Length; i++)
            {
                rightArmament.armamentArr[i].currentAmmo = rightArmament.armamentArr[i].maxAmmo;
            }
        }
        else
        {
            rightBioTech.currentHeat = rightBioTech.Heat;
            rightBioTech.currentMP = rightBioTech.MP;
            for (int i = 0; i < leftArmament.armamentArr.Length; i++)
            {
                leftArmament.armamentArr[i].currentAmmo = leftArmament.armamentArr[i].maxAmmo;
            }
        }

        yield return new WaitForSeconds(0.2f);
        PreAttack();
    }

    void PreAttack()
    {
        StartCoroutine(StartTurn());
        phase = "Attack";
        //armGroup.SetActive(true);

        if (actualTurn % 2 != 0)
        {
            //left can use armaments and move
            leftAction.BioOn(true);
            leftTable.TurnOffOn(false);
            leftAction.DmgOn(true);

            rightTable.TurnOffOn(true);
            rightUITable.SetActive(true);
            rightAction.BioOn(false);
            rightAction.DmgOn(false);
            combat.ChangeCanvas("left");
        }
        else
        {
            //right can use armaments and move
            leftTable.TurnOffOn(true);
            leftUITable.SetActive(true);
            leftAction.BioOn(false);
            leftAction.DmgOn(false);

            rightAction.BioOn(true);
            rightTable.TurnOffOn(false);
            rightAction.DmgOn(true);

            combat.ChangeCanvas("right");
        }
       
    }

    IEnumerator StartTurn()
    {
        yield return new WaitForSeconds(0.6f);

        if (StartTurnAction != null)
        {
            StartTurnAction();
        }
    }

    void Attack()
    {
        if (actualTurn % 2 != 0)
        {
            rightAction.DmgOn(true);
        }
        else
        {
            leftAction.DmgOn(true);
        }
    }

    void Defend()
    {
        if (EndPhaseAction != null)
        {
            EndPhaseAction();
        }
        if (EndAttackAction != null)
        {
            EndAttackAction();
        }
        //heavy armaments from the turn before fall now
        //wait 0.5s
        //armGroup.SetActive(false);
        cameras.ChangeCam();

        phase = "Defend";
        if (actualTurn % 2 != 0)
        {
            //right can move and use only reactive armamentes
            //left armaments have been used
            leftAction.BioOn(false);
            leftTable.TurnOffOn(false);

            combat.ChangeCanvas("right");

            rightAction.BioOn(true);
            rightTable.TurnOffOn(false);
            //right chooses when to end or ran out of MP
            //wait 0.5s
            //normal speed armaments fall now
        }
        else
        {
            //left can move and use only reactive armaments
            //right armaments have been used
            rightAction.BioOn(false);
            rightTable.TurnOffOn(false);

            combat.ChangeCanvas("left");

            leftAction.BioOn(true);
            leftTable.TurnOffOn(false);
            //left chooses when to end or ran out of MP
            //wait 0.5s
            //normal speed armaments fall now
        }
    }

    void PostCombat()
    {
        if (EndDefenseAction != null)
        {
            EndDefenseAction();
        }
        if (EndPhaseAction != null)
        {
            EndPhaseAction();
        }

        cameras.ChangeCam();
        phase = "End turn";
        if (actualTurn % 2 != 0)
        {
            //left can move
            leftAction.BioOn(true);

            combat.ChangeCanvas("left");

            rightAction.BioOn(false);
        }
        else
        {
            //right can move
            rightAction.BioOn(true);

            combat.ChangeCanvas("right");

            leftAction.BioOn(false);
        }
    }

    void End()
    {
        if (EndPhaseAction != null)
        {
            EndPhaseAction();
        }
        phase = "End";
        actualTurn++;
        if (EndTurnAction != null)
        {
            EndTurnAction(); 
        }
        cameras.ChangeCam();
        StartCoroutine(Provision());
    }

    public void EndPhase()
    {
        switch (phase)
        {
            case "Attack":
                Attack();
                if (EndAttackAction != null)
                {
                    EndAttackAction();
                }
                Defend();
                break;

            case "Defend":
                if (invisibleShot == true)
                {
                    StartCoroutine(Wait());
                }
                else
                {
                    PostCombat();
                }
                break;

            case "End turn":
                End();
                break;
        }
    }

    IEnumerator Wait()
    {
        if (RevealAction != null)
        {
            RevealAction();
        }
        yield return new WaitForSeconds(0.8f);
        invisibleShot = false;
        PostCombat();
    }

}
