using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer 
{
    void Check(int turn, int[] duration);
    void Count(int[] duration);
}
