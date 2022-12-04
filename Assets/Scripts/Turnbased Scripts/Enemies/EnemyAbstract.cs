using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbstract : MonoBehaviour,IDamageable,IHeal
{
    public EnemiesCharStatsBase myStats;
    public Animator myAnimator;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Newturn()
    {
        myStats.exhausted = false;
        myStats.turnCharges=myStats.turnCharges_Max;
    }

    public void Damage(float damage)
    {
        if (myStats.HP + damage < 0)
        {
            myStats.HP = myStats.maxHP;
        }
        else
        {
            myStats.HP  -= damage;
        }
    }
    public void Heal(float heal)
    {
        if (myStats.HP + heal > myStats.maxHP)
        {
            myStats.HP = myStats.maxHP;
        }
        else
        {
            myStats.HP  += heal;
        }
    }

      public void Death()
    {
        //death animation
        Destroy(this.gameObject,0.5f);       
    }
}
