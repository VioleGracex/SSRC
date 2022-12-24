using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFillHandler : MonoBehaviour
{
    [SerializeField] Image fillBarImage;
   [HideInInspector] public float barFillPercentage;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        fillBarImage.fillAmount = barFillPercentage;
    }
    public void SetBarFillPercentage(float newPercentage)
    {
        barFillPercentage = newPercentage;
    }
}
