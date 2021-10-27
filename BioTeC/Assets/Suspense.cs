using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspense : MonoBehaviour
{
    private Queue suspendImmediate = new Queue();
    private Queue immeadiateEffectsQue = new Queue();
    private Queue immeadiateHiddenQue = new Queue();

    private Queue suspendReactive = new Queue();
    private Queue ReactiveEffectsQue = new Queue();
    private Queue ReactiveHiddenQue = new Queue();

    private Queue suspendedHeavy = new Queue();
    private Queue heavyEffectsQue = new Queue();
    private Queue heavyHiddenQue = new Queue();

    IEffects2 effectsInterface;

    bool hide;
    int effects;

    bool heavy = false;
    bool reactive = false;
    bool immediate = false;

    int coordinate = 0;
    public int[] delay = new int [] {1, 3, 5};
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

   /* void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            foreach (int attack in heavyEffectsQue)
            {
                Debug.Log(" " + attack);
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
                    for (int i = 0; i < form.Length; i++)
                    {
                        heavyHiddenQue.Enqueue(true);
                        suspendedHeavy.Enqueue(form[i]);

                        coordinate += 1;
                    }

                    if (effectPosition.Length > 0)
                    {
                        for (int j = 0; j < effectPosition.Length; j++)
                        {
                            int p = effectPosition[j] - 1;

                            coordinate = coordinate - 1;
                            
                            heavyEffectsQue.Enqueue(form[p]);
                        }

                        if (coordinate != 0)
                        {
                            for (int i = form.Length; i > coordinate; i--)
                            {
                                heavyEffectsQue.Enqueue(0);
                            }
                            coordinate = 0;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < form.Length; i++)
                    {
                        suspendedHeavy.Enqueue(form[i]);
                        heavyHiddenQue.Enqueue(false);

                        coordinate += 1;
                    }

                    if (effectPosition.Length > 0)
                    {
                        for (int j = 0; j < effectPosition.Length; j++)
                        {
                            int p = effectPosition[j] - 1;

                            coordinate -= 1;

                            heavyEffectsQue.Enqueue(form[p]);
                        }

                        if (coordinate != 0)
                        {
                            for (int i = form.Length; i > coordinate; i--)
                            {
                                heavyEffectsQue.Enqueue(0);
                            }
                            coordinate = 0;
                        }
                    }
                }

                break;

            case "Immediate":
                if (invisible == true)
                {
                    for (int i = 0; i < form.Length; i++)
                    {
                        immeadiateHiddenQue.Enqueue(true);
                        suspendImmediate.Enqueue(form[i]);

                        coordinate += 1;
                    }

                    if (effectPosition.Length > 0)
                    {
                        for (int j = 0; j < effectPosition.Length; j++)
                        {
                            int p = effectPosition[j] - 1;

                            coordinate = coordinate - 1;

                            immeadiateEffectsQue.Enqueue(form[p]);
                        }

                        if (coordinate != 0)
                        {
                            for (int i = form.Length; i > coordinate; i--)
                            {
                                immeadiateEffectsQue.Enqueue(0);
                            }
                            coordinate = 0;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < form.Length; i++)
                    {
                        suspendImmediate.Enqueue(form[i]);
                        immeadiateHiddenQue.Enqueue(false);

                        coordinate += 1;
                    }

                    if (effectPosition.Length > 0)
                    {
                        for (int j = 0; j < effectPosition.Length; j++)
                        {
                            int p = effectPosition[j] - 1;

                            coordinate -= 1;

                            immeadiateEffectsQue.Enqueue(form[p]);
                        }

                        if (coordinate != 0)
                        {
                            for (int i = form.Length; i > coordinate; i--)
                            {
                                immeadiateEffectsQue.Enqueue(0);
                            }
                            coordinate = 0;
                        }
                    }
                }
                break;

            case "Reactive":
                if (invisible == true)
                {
                    for (int i = 0; i < form.Length; i++)
                    {
                        ReactiveHiddenQue.Enqueue(true);
                        suspendReactive.Enqueue(form[i]);

                        coordinate += 1;
                    }

                    if (effectPosition.Length > 0)
                    {
                        for (int j = 0; j < effectPosition.Length; j++)
                        {
                            int p = effectPosition[j] - 1;

                            coordinate = coordinate - 1;

                            ReactiveEffectsQue.Enqueue(form[p]);
                        }

                        if (coordinate != 0)
                        {
                            for (int i = form.Length; i > coordinate; i--)
                            {
                                ReactiveEffectsQue.Enqueue(0);
                            }
                            coordinate = 0;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < form.Length; i++)
                    {
                        suspendReactive.Enqueue(form[i]);
                        ReactiveHiddenQue.Enqueue(false);

                        coordinate += 1;
                    }

                    if (effectPosition.Length > 0)
                    {
                        for (int j = 0; j < effectPosition.Length; j++)
                        {
                            int p = effectPosition[j] - 1;

                            coordinate -= 1;

                            ReactiveEffectsQue.Enqueue(form[p]);
                        }

                        if (coordinate != 0)
                        {
                            for (int i = form.Length; i > coordinate; i--)
                            {
                                ReactiveEffectsQue.Enqueue(0);
                            }
                            coordinate = 0;
                        }
                    }
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
                    delay[0] = 2;
                    immediate = true;
                }

                if (delay[1] == 0)
                {
                    delay[1] = 3;
                    reactive = true;
                }

                if (delay[2] == 0)
                {
                    delay[2] = 6;
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
        if (immediate == true)
        {
            foreach (bool attack in immeadiateHiddenQue)
            {
                hide = attack;
                immeadiateHiddenQue.Dequeue();
                break;
            }

            foreach (int effect in immeadiateEffectsQue)
            {
                effects = effect;
                immeadiateEffectsQue.Dequeue();
                break;
            }

            foreach (int attack in suspendImmediate)
            {
                effectsInterface.Effect(attack, effects, hide);
                suspendImmediate.Dequeue();
                break;
            }

            if (suspendImmediate.Count > 0)
            {
                Send();
            }
            else
            {
                immediate = false;
                hide = false;
            }
        }

        if (heavy == true)
        {
            foreach (bool attack in heavyHiddenQue)
            {
                hide = attack;
                heavyHiddenQue.Dequeue();
                break;
            }

            foreach (int effect in heavyEffectsQue)
            {
                effects = effect;
                heavyEffectsQue.Dequeue();
                break;
            }
            foreach (int attack in suspendedHeavy)
            {
                effectsInterface.Effect(attack, effects, hide);
                suspendedHeavy.Dequeue();
                break;
            }

            if(suspendedHeavy.Count > 0)
            {
                Send();
            }
            else
            {
                heavy = false;
                hide = false;
            }
        }

        if(reactive == true)
        {
            foreach (bool attack in ReactiveHiddenQue)
            {
                hide = attack;
                ReactiveHiddenQue.Dequeue();
                break;
            }

            foreach (int effect in ReactiveEffectsQue)
            {
                effects = effect;
                ReactiveEffectsQue.Dequeue();
                break;
            }

            foreach (int attack in suspendReactive)
            {
                effectsInterface.Effect(attack, effects, hide);
                suspendReactive.Dequeue();
                break;
            }

            if (suspendReactive.Count > 0)
            {
                Send();
            }
            else
            {
                reactive = false;
                hide = false;
            }
        }
    }
}
