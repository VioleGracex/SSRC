using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAbstract : MonoBehaviour,IDamageable,IHeal
{
    public HerosCharStatsBase myStats;
    public Animator myAnimator;

    public float attack,defense,dex,HP,exp;

    public int turnCharges,level,weaponMastry;

    public bool exhausted;
    // Start is called before the first frame update
    void Start()
    {
        myStats.myPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Newturn()
    {
        exhausted = false;
        turnCharges=myStats.turnCharges_Max;
    }
    public void Damage(float damage)
    {
        if (HP - damage < 0)
        {
            HP = 0;//death
        }
        else
        {
            HP  -= damage;
        }
    }
    public void Heal(float heal)
    {
        if (HP + heal > myStats.maxHP)
        {
            HP = myStats.maxHP;
        }
        else
        {
            HP  += heal;
        }
    }

      public void Death()
    {
        //death animation
        Destroy(this.gameObject,0.5f);       
    }
}
