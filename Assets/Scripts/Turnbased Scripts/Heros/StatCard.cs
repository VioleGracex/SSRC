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
    Image heroImg;
    [SerializeField]
    Transform chargesContentHolder;
    int maxCharges;
    public HeroAbstract myHero;

    // Start is called before the first frame update
    public void InitStatCard()
    {
        hpSlider.maxValue = myHero.myStats.maxHP;
        hpSlider.value = myHero.HP;
        spSlider.maxValue = myHero.myStats.maxSP;
        spSlider.value = myHero.SP;
        heroName.text = myHero.myStats.unitName;
        maxCharges = myHero.myStats.turnCharges_Max;
        heroImg.sprite = myHero.myStats.cardSprite;
        CorrectChargesCount();
    }

    private void CorrectChargesCount()
    {
        //delete from last not like this
        for(int i = chargesContentHolder.childCount-maxCharges; i > 0 ; i--)
        {
            Destroy(chargesContentHolder.GetChild((chargesContentHolder.childCount-i)).gameObject);
        }
        UpdateTurnCharges();
    }
    public void UpdateStatus()
    {
        hpSlider.value = myHero.HP;
        spSlider.value = myHero.SP;
        UpdateTurnCharges();
    }

    public void UpdateTurnCharges()
    {
        for(int i = 0 ; i < myHero.GetTurnCharges() ; i++)
        {
            chargesContentHolder.GetChild(i).GetComponent<ChargeChanger>().TurnOn();
        }   
        for(int i = myHero.GetTurnCharges() ; i < maxCharges ; i++)
        {
            chargesContentHolder.GetChild(i).GetComponent<ChargeChanger>().TurnOff();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
