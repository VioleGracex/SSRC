using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeChanger : MonoBehaviour
{
    [SerializeField]
    Image myImg;
    [SerializeField]
    Sprite offCharge, onCharge;
    [SerializeField]
    Outline myOutliner;

    bool isOn;
    public void SwitchAvailability()
    {
        if(isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public void TurnOff()
    {
        myImg.sprite = offCharge;
        myOutliner.enabled = false;
        isOn = false;
    }

    public void TurnOn()
    {
        myImg.sprite = onCharge;
        myOutliner.enabled = true;
        isOn = true;
    }
}
