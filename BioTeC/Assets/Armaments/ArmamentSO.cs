using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Speed { Reactive, Immediate, Heavy };
[CreateAssetMenu(fileName = "New Armament", menuName = "Add Armament")]
public class ArmamentSO : ScriptableObject
{
    [SerializeField] public int damage;
    [SerializeField] public int extraDamage;
    [SerializeField] public int[] positionExtraDamage;

    [SerializeField] public int currentAmmo;
    [SerializeField] public int maxAmmo;

    //[SerializeField] public string faction;
    [SerializeField] public string armamentName;

    //[SerializeField] public bool innate;
    public Speed speed; 
    [SerializeField] public bool invisible;
    [SerializeField] public bool estatico;
    public int Heat;

    [SerializeField] public int[] form;
    [SerializeField] public string effect;
    [SerializeField] public int[] effectPosition;
    [SerializeField] public int[] NonSelectableBlocks;

    [SerializeField] public Sprite formImage;

}
