using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentClass : MonoBehaviour
{
    public delegate void LaunchAct(int damage, int[] positions, string speed);
    public static event LaunchAct LaunchAction;

    int damage;   
    int ammo;
    //int effect = 0;

    bool extraDamage = false;

    string faction;
    string armamentName;

    bool innate;
    bool heavy;
    bool invisible;
    bool estatico;
    public bool valid = false;

    [SerializeField] public ArmamentSO[] armamentArr =  new ArmamentSO[6];
    [SerializeField] public ArmamentSO armament = null;
    //[SerializeField] Table table = null;
    [SerializeField] UITable UItable = null;
    [SerializeField] BioTechSO playerBioTech = null;

    [SerializeField] Suspense suspend = null;

    public int weaponEnable;

    //public Queue order = new Queue();

    int j = 0;

    void OnEnable()
    {
        WeaponMode.WeaponModeAction += ChangeStance;
    }

    void OnDisable()
    {
        WeaponMode.WeaponModeAction -= ChangeStance;
    }

    public void Show(int x)
    {
        armament = armamentArr[x];
        for (int i = 0; i <= UItable.UItable.Length - 1; i++)
        {
            //table.table[i].SetActive(true);
            UItable.UItable[i].SetActive(true);
        }

        for (int i = 0; i < armament.form.Length; i++)
        {
            j = armament.form[i] - 1;
            //table.table[j].SetActive(false);
            UItable.UItable[j].SetActive(false);
            //Debug.Log("" + boxes[j].ToString());
        }
    }

    void ValidBox()
    {
        valid = true;

        for (int i = 0; i < armament.NonSelectableBlocks.Length; i++)
        {
            for (int j = 0; j< armament.form.Length; j++)
            {
                if(armament.NonSelectableBlocks[i] == armament.form[j])
                {
                    valid = false;
                    break;
                }
                else if (valid != false)
                {
                    valid = true;
                }
            }
        }
    }


    void Update()
    {
        if (weaponEnable == 1)
        {
            if (armament.estatico == false)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Aim("right");
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Aim("left");
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Aim("up");
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Aim("down");
                } 
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && armament.currentAmmo > 0)
            {
                ValidBox();
                if (valid == true)
                {
                    Launch();
                    armament.currentAmmo--;
                }
                else
                {
                    Debug.Log("la casilla no es valida");
                }
            }
        }
    }

    void ChangeStance()
    {
        if (weaponEnable == 0)
        {
            weaponEnable = 1;
        }
        
        else if (weaponEnable == 1)
        {
            weaponEnable = 0;
        }
    }

    void Launch()
    {
        if ((playerBioTech.currentHeat - armament.Heat) >= 0)
        {
            playerBioTech.currentHeat = playerBioTech.currentHeat - armament.Heat;

            if (armament.speed.ToString() == "Heavy")
            {
                suspend.Suspend(armament.form, armament.effectPosition, armament.invisible, "Heavy");
            }

            if (armament.speed.ToString() == "Immediate")
            {
                suspend.Suspend(armament.form, armament.effectPosition, armament.invisible, "Immediate");
            }

            if (armament.speed.ToString() == "Reactive")
            {
                suspend.Suspend(armament.form, armament.effectPosition, armament.invisible, "Reactive");
            }

            for (int i = 0; i < playerBioTech.currentPosition.Length; i++)
            {
                if (extraDamage == false)
                {
                    for (int j = 0; j < armament.positionExtraDamage.Length; j++)
                    {
                        if (playerBioTech.currentPosition[i] == armament.positionExtraDamage[j])
                        {
                            extraDamage = true;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            if (extraDamage == true)
            {
                //Debug.Log("in");
                if (LaunchAction != null)
                {
                    LaunchAction(armament.damage + armament.extraDamage, armament.form, armament.speed.ToString());
                }
                extraDamage = false;
            }
            else
            {
                //Debug.Log("out");
                if (LaunchAction != null)
                {
                    LaunchAction(armament.damage, armament.form, armament.speed.ToString());
                }
            }
        }
        else
        {
            Debug.Log("not enough heat");
        }
    }

    void Aim(string x)
    {
        if (x == "right")
        {
            for (int i = 0; i <= armament.form.Length - 1; i++)
            {
                if ((armament.form[i] == 1) || (armament.form[i] == 6) || (armament.form[i] == 11) || (armament.form[i] == 16) || (armament.form[i] == 21))
                {
                    Debug.Log("out of bounds right");
                    break;
                }
                else if (i == armament.form.Length -1)
                {
                    Right();
                } 
            }
        }
        if (x == "left")
        {
            for (int i = 0; i <= armament.form.Length - 1; i++)
            {
                if ((armament.form[i] == 5) || (armament.form[i] == 10) || (armament.form[i] == 15) || (armament.form[i] == 20) || (armament.form[i]==25))
                {
                     Debug.Log("out of bounds left");
                     break;
                }
                else if (i == armament.form.Length - 1)
                {
                     Left();
                } 
            }
        }
        if (x == "up")
        {
            for (int i = 0; i <= armament.form.Length - 1; i++)
            {
                if ((armament.form[i] == 21) || (armament.form[i] == 22) || (armament.form[i] == 23) || (armament.form[i] == 24) || (armament.form[i] == 25))
                {
                    Debug.Log("out of bounds up");
                    break;
                }
                else if (i == armament.form.Length - 1)
                {
                    Up();
                }
            }
        }
        if (x == "down")
        {
            for (int i = 0; i <= armament.form.Length - 1; i++)
            {
                if ((armament.form[i] == 1) || (armament.form[i] == 2) || (armament.form[i] == 3) || (armament.form[i] == 4) || (armament.form[i] == 5))
                {
                    Debug.Log("out of bounds down");
                    break;
                }
                else if (i == armament.form.Length - 1)
                {
                    Down();
                }
            }
        }
    }

    void Right()
    {
       for (int i = 0; i <= armament.form.Length - 1; i++)
       {
            //table.table[armament.form[i] - 1].SetActive(true);
            UItable.UItable[armament.form[i] - 1].SetActive(true);
            //Debug.Log("prendidas " + boxes[form[i]].ToString());
            armament.form[i] = armament.form[i] - 1;
           //Debug.Log("" + form[i].ToString());
       }
       for (int i = 0; i < armament.form.Length; i++)
       {
          j = armament.form[i] - 1;
          //table.table[j].SetActive(false);
          UItable.UItable[j].SetActive(false);
          //Debug.Log("" + boxes[j].ToString());
       }
    }

    void Left()
    {
       for (int i = 0; i <= armament.form.Length - 1; i++)
       {
            //table.table[armament.form[i] - 1].SetActive(true);
            UItable.UItable[armament.form[i] - 1].SetActive(true);
            //Debug.Log("prendidas " + boxes[form[i]].ToString());
            armament.form[i] = armament.form[i] + 1;
          //Debug.Log("" + form[i].ToString());
       }
       for (int i = 0; i < armament.form.Length; i++)
       {
           j = armament.form[i] - 1;
           //table.table[j].SetActive(false);
           UItable.UItable[j].SetActive(false);
           //Debug.Log("" + boxes[j].ToString());
        }
    }
    void Up()
    {
        for (int i = 0; i <= armament.form.Length - 1; i++)
        {
            //table.table[armament.form[i] - 1].SetActive(true);
            UItable.UItable[armament.form[i] - 1].SetActive(true);
            //Debug.Log("prendidas " + boxes[form[i]].ToString());
            armament.form[i] = armament.form[i] + 5;
            //Debug.Log("" + form[i].ToString());
        }
        for (int i = 0; i < armament.form.Length; i++)
        {
            j = armament.form[i] - 1;
            //table.table[j].SetActive(false);
            UItable.UItable[j].SetActive(false);
            //Debug.Log("" + boxes[j].ToString());
        }
    }
    void Down()
    {
        for (int i = 0; i <= armament.form.Length - 1; i++)
        {
            //table.table[armament.form[i] - 1].SetActive(true);
            UItable.UItable[armament.form[i] - 1].SetActive(true);
            //Debug.Log("prendidas " + boxes[form[i]].ToString());
            armament.form[i] = armament.form[i] - 5;
            //Debug.Log("" + form[i].ToString());
        }
        for (int i = 0; i < armament.form.Length; i++)
        {
            j = armament.form[i] - 1;
            //table.table[j].SetActive(false);
            UItable.UItable[j].SetActive(false);
            //Debug.Log("" + boxes[j].ToString());
        }
    }
}
