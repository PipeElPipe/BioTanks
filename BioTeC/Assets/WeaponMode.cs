using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMode : MonoBehaviour
{
    public delegate void WeaponModeChange();
    public static event WeaponModeChange WeaponModeAction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (WeaponModeAction != null)
            {
                WeaponModeAction();
            }
            ToolTipSystem.Hide();
        }
    }

}
