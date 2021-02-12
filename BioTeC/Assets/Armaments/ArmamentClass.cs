using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentClass : MonoBehaviour // public class ArmamentClass : Armament ?
{
    int damage;   
    int ammo;
    int effect;
    //int[] form;?

    string faction;
    string name;

    bool innate;
    bool heavy;
    bool invisible;
    bool estatico;
    

    void Start()
    {
        switch (effect) //
        {
            case 0:
                // has no effects
                break;
            case 1:
                //incendiary
                //lights cells on fire
                //the armament itself determines which cells will have the effect, this class only describes the effect.
                break;
            case 2:
                //corrosive
                //creates cells to ROOT whatever stands on them
                //ROOT will leave the target unmovible for 3 turns.
                //the armament itself determines which cells will have the effect, this class only describes the effect.
                break;
            case 3:
                //collapse
                //collapsed cells are no longer available for the player to move.
                //the armament itself determines which cells will have the effect, this class only describes the effect.
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
