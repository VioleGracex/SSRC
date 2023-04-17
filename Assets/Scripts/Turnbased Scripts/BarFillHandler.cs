using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFillHandler : MonoBehaviour
{
    [SerializeField] Image fillBarImage;
    [HideInInspector] float barFillPercentage;
    
    public void SetBarFillPercentage(float newPercentage)
    {
        barFillPercentage = newPercentage;
    }
}
