using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatCard : MonoBehaviour
{

    [SerializeField]
    Slider hpSlider, spSlider;
    [SerializeField]
    TextMeshProUGUI heroName;
    [SerializeField]
    Image heroImg, hpFiller;
    [SerializeField]
    GameObject chargeAvailable, chargeUsed; 
    Transform chargesContentHolder;
    int maxCharges, currentCharges;
    HeroAbstract myHero;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.maxValue = myHero.myStats.maxHP;
        hpSlider.value = myHero.HP;
        spSlider.maxValue = myHero.myStats.maxSP;
        spSlider.value = myHero.SP;
        heroName.text = myHero.myStats.name;
        maxCharges = myHero.myStats.turnCharges_Max;
        currentCharges = myHero.turnCharges;
        heroImg.sprite = myHero.myStats.cardSprite;
        InitTurnCharges();
    }

    public void UpdateStatus()
    {
        hpSlider.value = myHero.HP;
        spSlider.value = myHero.SP;
        currentCharges = myHero.turnCharges;
        UpdateTurnCharges();
    }

    void InitTurnCharges()
    {
        for(int i = 0 ; i<currentCharges ; i++)
        {
            Instantiate(chargeAvailable, chargesContentHolder);
        }
        for(int i = currentCharges ; i<maxCharges ; i++)
        {
            Instantiate(chargeUsed, chargesContentHolder);
        }
    }

    void UpdateTurnCharges()
    {
        for(int i = 0 ; i<currentCharges ; i++)
        {
            chargesContentHolder.GetChild(i).GetComponent<ChargeChanger>().TurnOn();
        }
        for(int i = currentCharges ; i<maxCharges ; i++)
        {
            chargesContentHolder.GetChild(i).GetComponent<ChargeChanger>().TurnOff();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
