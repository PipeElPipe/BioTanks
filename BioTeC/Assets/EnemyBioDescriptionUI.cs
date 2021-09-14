using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBioDescriptionUI : MonoBehaviour
{
    [SerializeField] Text bioNameDes = null;
    [SerializeField] Text bioHeatDes = null;
    [SerializeField] Text bioMoveDes = null;
    [SerializeField] Text bioSizeDes = null;
    //[SerializeField] Image bioHPDes = null;
    [SerializeField] Text bioArmorDes = null;

    [SerializeField] BioTechSO bioTech = null;

    [SerializeField] Slider HPslider = null;

    void SetMaxHealth()
    {
        HPslider.maxValue = bioTech.maxHP;
        HPslider.value = bioTech.maxHP;
    }

    void SetHealth()
    {
        HPslider.value = bioTech.currentHP;
    }

    void Update()
    {
        bioHeatDes.text = "Heat Points: " + bioTech.currentHeat.ToString() + "/" + bioTech.Heat.ToString();
        bioMoveDes.text = "Move Points: " + bioTech.currentMP.ToString() + "/" + bioTech.MP.ToString();

        HPslider.maxValue = bioTech.maxHP;
        HPslider.value = bioTech.currentHP;

        bioNameDes.text = "" + bioTech.name.ToString();
        bioSizeDes.text = "Size: " + bioTech.size.ToString();
        bioArmorDes.text = "Armor: " + bioTech.armor.ToString();
    }
}
