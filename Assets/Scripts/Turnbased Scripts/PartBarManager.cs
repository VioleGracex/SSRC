using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PartBarManager : MonoBehaviour
{
    [SerializeField]
    Slider mySlider;
    [SerializeField]
    Transform armorHolder;
    [SerializeField]
    TextMeshProUGUI partName;

    public void PartBarInit(float maxHP, float currentHP , float currentArmor, string name) //max armor is 4 by default
    {
        mySlider.maxValue = maxHP;
        mySlider.value = currentHP;
        partName.text = name;
        for(int i = 0 ; i < currentArmor ; i++)
        {
            armorHolder.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void CloseBarsView()
    {
        this.transform.parent.parent.gameObject.SetActive(false);
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
