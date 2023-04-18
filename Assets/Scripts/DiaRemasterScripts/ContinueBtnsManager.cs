using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueBtnsManager : MonoBehaviour
{
    [SerializeField]
    Button[] btns;
    public void DisableAllBtns()
    {
        foreach(var btn in btns)
        {
            btn.interactable = false;
        }
    }

    public void EnableAllBtns()
    {
        foreach(var btn in btns)
        {
            btn.interactable = true;
        }
    }
}
