using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BioDescriptionUI : MonoBehaviour
{
    [SerializeField] Text bioNameDes = null;
    [SerializeField] Image bioHeatDes = null;
    [SerializeField] Text bioMoveDes = null;
    [SerializeField] Text bioSizeDes = null;
    [SerializeField] Image bioHPDes = null;
    [SerializeField] Text bioArmorDes = null;

    [SerializeField] BioTechSO bioTech = null;

    // Start is called before the first frame update
    void Start()
    {
        bioNameDes.text = "" + bioTech.name.ToString();
        //bioHeatDes.text = "" + bioTech.Heat.ToString();
        bioMoveDes.text = "MP: " + bioTech.MP.ToString();
        bioSizeDes.text = "" + bioTech.size.ToString();
        //bioHPDes.text = "" + bioTech.currentHP.ToString();
        bioArmorDes.text = "" + bioTech.armor.ToString();
    }
}
