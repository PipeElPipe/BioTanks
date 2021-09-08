using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject leftcam = null;
    [SerializeField] GameObject rightcam = null;

    [SerializeField] GameObject UpLeftcam = null;
    [SerializeField] GameObject UpRightcam = null;

    void OnEnable()
    {
        WeaponMode.WeaponModeAction += UpCam;
        TurnSystem.EndPhaseAction += UpCameraOFF;
    }

    void OnDisable()
    {
        WeaponMode.WeaponModeAction -= UpCam;
        TurnSystem.EndPhaseAction -= UpCameraOFF;
    }

    void UpCam()
    {
        if (leftcam.activeSelf == true)
        {
            if(UpLeftcam.activeSelf == true)
            {
                UpLeftcam.SetActive(false);
            }
            else
            {
                UpLeftcam.SetActive(true);
            }
        }

        if (rightcam.activeSelf == true)
        {
            if (UpRightcam.activeSelf == true)
            {
                UpRightcam.SetActive(false);
            }
            else
            {
                UpRightcam.SetActive(true);
            }
        }
    }

    void UpCameraOFF()
    {
        UpRightcam.SetActive(false);
        UpLeftcam.SetActive(false);
    }

    public void ChangeCam()
    {
        if(leftcam.activeSelf == true)
        {
            leftcam.SetActive(false);
        }
        else
        {
            leftcam.SetActive(true);
        }

        if (rightcam.activeSelf == true)
        {
            rightcam.SetActive(false);
        }
        else
        {
            rightcam.SetActive(true);
        }
    }
}
