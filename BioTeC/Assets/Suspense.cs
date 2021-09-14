using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspense : MonoBehaviour
{
    private Queue suspendedHeavy = new Queue();

    IEffects2 effectsInterface;

    bool hide;
    bool effect;
    //int[] effectArr = new int [26];

    private Queue effectsQue = new Queue();

    bool heavy = false;
    bool reactive = false;
    bool immediate = false;


    int[] delay = new int [] {1, 3, 5};
    //1 = immediate, 3 = reactive, 5 = heavy

    void OnEnable()
    {
        effectsInterface = GetComponent<IEffects2>();
        TurnSystem.EndPhaseAction += CountDown;
    }

    void OnDisable()
    {
        TurnSystem.EndPhaseAction -= CountDown;
    }

    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            foreach(int attack in suspendedHeavy.ToArray())
            {
                Debug.Log("golpean en:" + attack.ToString());
            }

            for (int i = 0; i< effectArr.Length; i++)
            {
                if (effectArr[i] != 0)
                {
                    Debug.Log("efectos en:"+ effectArr[i]); 
                }
            }
        }
    }*/

    public void Suspend(int[] form, int[] effectPosition, bool invisible, string speed)
    {
        UISend(form, effectPosition);

        switch(speed)
        {
            case "Heavy":
                if(invisible == true)
                {
                    hide = true;
                }

                for (int i = 0; i < form.Length; i++)
                {
                    suspendedHeavy.Enqueue(form[i]);

                    if (effectPosition.Length > 0)
                    {
                        for (int j = 0; j < effectPosition.Length; j++)
                        {
                            int p = effectPosition[j] - 1;

                            //effectArr[j] = form[p];
                            effectsQue.Enqueue(form[p]);
                        } 
                    }
                }

                break;

            case "Immediate":
                if (invisible == true)
                {
                    hide = true;
                }

                break;
        }
    }

    void CountDown()
    {
        for (int i = 0; i < delay.Length; i++)
        {
            delay[i] = delay[i] - 1;
            if(delay[i] == 0)
            {
                if (delay[0] == 0)
                {
                    delay[0] = 1;
                    immediate = true;
                }

                if (delay[1] == 0)
                {
                    delay[1] = 3;
                    reactive = true;
                }

                if (delay[2] == 0)
                {
                    delay[2] = 5;
                    heavy = true;
                }

                Send();
            }
        }
    }

    void UISend(int[] form, int[] effectPosition)
    {
        effectsInterface.UIEffect(form, effectPosition);
    }

    void Send()
    {
        if (heavy == true)
        {
            foreach (int attack in suspendedHeavy)
            {
                foreach (int effect in effectsQue)
                {
                    effectsInterface.Effect(attack, effect, hide);
                }                
            }
            heavy = false;
            hide = false;
        }
    }
}
