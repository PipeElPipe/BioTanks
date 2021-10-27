using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUI : MonoBehaviour
{
    [SerializeField] ArmamentClass armamentClass = null;
    [SerializeField] Text nameDes = null;
    [SerializeField] Text damageDes = null;
    [SerializeField] Text heatDes = null;
    [SerializeField] Text ammoDes = null;

    [SerializeField] GameObject immediate = null;
    [SerializeField] GameObject heavy = null;
    [SerializeField] GameObject reactive = null;

    [SerializeField] GameObject hidden = null;
    [SerializeField] GameObject visible = null;

    [SerializeField] Text effectDes = null;

    [SerializeField] GameObject aim = null;
    [SerializeField] GameObject locked = null;

    [SerializeField] GameObject descriptionText = null;

    [SerializeField] Image formImage = null;

    string speed = null;

    void OnEnable()
    {
        TurnSystem.EndAttackAction += DescriptionOff;
    }

    void OnDisable()
    {
        TurnSystem.EndAttackAction -= DescriptionOff;
    }

    void Update()
    {
        speed = armamentClass.armament.speed.ToString();
        if (armamentClass.weaponEnable == 1)
        {
            descriptionText.SetActive(true);

            if (armamentClass.armament.formImage != null)
            {
                formImage.sprite = armamentClass.armament.formImage; 
            }
            nameDes.text = armamentClass.armament.name;
            damageDes.text = "Damage: " + armamentClass.armament.damage.ToString();
            heatDes.text = "Heat: " + armamentClass.armament.Heat.ToString();
            ammoDes.text = "Ammo: " + armamentClass.armament.currentAmmo.ToString();

            //speedDes.text = armamentClass.armament.speed.ToString();

            switch (speed)
            {
                case "Immediate":
                    immediate.SetActive(true);
                    heavy.SetActive(false);
                    reactive.SetActive(false);
                    break;

                case "Heavy":
                    heavy.SetActive(true);
                    immediate.SetActive(false);
                    reactive.SetActive(false);
                    break;

                case "Reactive":
                    reactive.SetActive(true);
                    heavy.SetActive(false);
                    immediate.SetActive(false);
                    break;
            }


            if (armamentClass.armament.invisible == true)
            {
                hidden.SetActive(true);
                visible.SetActive(false);
            }
            else
            {
                hidden.SetActive(false);
                visible.SetActive(true);
            }

            effectDes.text = armamentClass.armament.effect.ToString();

            if(armamentClass.armament.effect.ToString() == "")
            {
                effectDes.text = "no effect";
            }

            if (armamentClass.armament.estatico == true)
            {
                locked.SetActive(true);
                aim.SetActive(false);
            }
            else
            {
                locked.SetActive(false);
                aim.SetActive(true);
            }
        }
        else
        {
            descriptionText.SetActive(false);
        }
    }

    void DescriptionOff()
    {
        descriptionText.SetActive(false);
    }
}
