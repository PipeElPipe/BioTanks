using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour, ITimer
{
    public int duration = 3;
    public int[] singularTracking;
    public bool[] register;

    void Start()
    {
        singularTracking = new int[duration];
        register = new bool[duration];
    }

    public void Check(int turn, int[] time)
    {
        for(int i = 0; i < duration; i++)
        {
            while (turn > duration)
            {
                turn = turn - duration; 
            }
            if(i + 1 == turn)
            {
                register[i] = true;
                if (register[i] == true)
                {
                    singularTracking[i] = duration;
                }
            }
        }
        Count(time);
    }

    public void Count(int[] time)
    {
        for (int i = 0; i < duration; i++)
        {
            if (register[i] == true)
            {
                singularTracking[i]--;
                if (singularTracking[i] == 0)
                {
                    register[i] = false;
                }
            }
        }
    }
}
