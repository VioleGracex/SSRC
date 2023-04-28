using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PartBarManager : MonoBehaviour
{
    [SerializeField]
    Slider mySlider;
    [SerializeField]
    Transform armorHolder;

    public void PartBarInit(float maxHP, float currentHP , float currentArmor) //max armor is 4 by default
    {
        mySlider.maxValue = maxHP;
        mySlider.value = currentHP;
        for(int i = 0 ; i < currentArmor ; i++)
        {
            armorHolder.GetChild(i).gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
