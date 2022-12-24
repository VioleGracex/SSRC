using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroAbstract : MonoBehaviour,IDamageable,IHeal,IAttack,ICharges,IReturnTurnCharges,IReturnPosition
{
    public HerosCharStatsBase myStats;
    public Animator myAnimator;

    public float attack,defense,dex,HP,exp;

    public int turnCharges,level,weaponMastry;

    public bool exhausted;

    public GameObject charToken;
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
    public void Attack(GameObject attackTarget)
    {
        attackTarget.GetComponent<IDamageable>().Damage(attack);
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

    void UpdateHPBar()
    {
        charToken.GetComponentInChildren<BarFillHandler>().SetBarFillPercentage(HP/myStats.maxHP);
    }
}
