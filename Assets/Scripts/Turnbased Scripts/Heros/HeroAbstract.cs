using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroAbstract : MonoBehaviour,IDamageable,IHeal,IAttack,ICharges,IReturnTurnCharges,IReturnPosition
{
    public HerosCharStatsBase myStats;
    public Animator myAnimator;

    public float attack, defense, dex, HP, SP, exp;

    public int turnCharges, level, weaponMastry;

    public bool exhausted;

    public GameObject charToken;
    // Start is called before the first frame update
    void Awake()
    {
        myStats.myPosition = this.transform.position;
        InitializeHeroStats();
    }

    void InitializeHeroStats()
    {
        // add level modifier
        level = myStats.level;
        attack = myStats.attack + ((level-1)*0.5f);
        defense = myStats.defense + ((level-1)*0.5f);
        dex = myStats.dex + ((level-1)*0.5f);
        HP = myStats.maxHP + ((level-1)*0.5f) ;
        SP = myStats.maxSP + ((level-1)*0.5f);
    }

    public void Newturn()
    {
        exhausted = false;
        turnCharges=myStats.turnCharges_Max;
    }
    public void Attack(GameObject attackTarget, string part)
    {
        attackTarget.GetComponent<IDamageable>().Damage(attack, part);
    }
    public void Damage(float damage,string part)
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

    public void Charges(int usage)
    {
        turnCharges -= usage;
    }
    public int ReturnCharges()
    {
        return turnCharges;
    }
    public Vector2 ReturnPosition()
    {
        return myStats.myPosition;
    }

      public void Death()
    {
        //death animation
        Destroy(this.gameObject,0.5f);       
    }

    void UpdateBars()
    {
        BarFillHandler[] bars = charToken.GetComponentsInChildren<BarFillHandler>();
        bars[0].SetBarFillPercentage(HP/myStats.maxHP);
        bars[1].SetBarFillPercentage(SP/myStats.maxSP); //stamina for skills 
        // update turn charges
    }
}
