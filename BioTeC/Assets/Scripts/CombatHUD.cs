using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    [SerializeField] GameObject leftTurnCanvas=null;
    [SerializeField] GameObject rightTurnCanvas=null;

    [SerializeField] Text phase = null;
    [SerializeField] Text mode = null;

    [SerializeField] TurnSystem turn = null;



    public void SetHUD()
    {
        /*
        nameText.text = bioTech.bioName;
        hpSlider.maxValue = bioTech.maxHP;
        hpSlider.value = bioTech.currentHP;
        */
    }
    
    public void ChangeCanvas(string turn)
    {
        if (turn == "left")
        {
            leftTurnCanvas.SetActive(true);
            rightTurnCanvas.SetActive(false); 
        }
        if (turn == "right")
        {
            leftTurnCanvas.SetActive(false);
            rightTurnCanvas.SetActive(true);
        }
    }

    void Update()
    {
        phase.text = turn.phase;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndPhase();
        }
    }

    public void EndPhase()
    {
        mode.text = "Weapons: Off";
        turn.EndPhase();
    }

    public void SetHP(int hp)
    {
        //hpSlider.value = hp;
    }
}
