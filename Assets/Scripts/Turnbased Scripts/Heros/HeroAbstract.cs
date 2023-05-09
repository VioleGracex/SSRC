using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroAbstract : MonoBehaviour,IDamageable,IHeal,IAttack,ICharges,IReturnTurnCharges,IReturnPosition
{
    public HerosCharStatsBase myStats;
    public Animator myAnimator;

    public float attack, defense, dex, HP, SP, exp;

    [SerializeField]
    private int turnCharges, level, weaponMastry;

    public bool exhausted;

    [SerializeField]
    StatCard myStatCard;

    [SerializeField]
    GameObject statCardTemplate;

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
        myStatCard = Instantiate(statCardTemplate, GameObject.FindGameObjectWithTag("CharCards").transform).GetComponent<StatCard>();
        myStatCard.myHero = this;
        myStatCard.InitStatCard();
    }

    public void Newturn()
    {
        exhausted = false;
        turnCharges = myStats.turnCharges_Max;
        myStatCard.UpdateTurnCharges();
        Debug.Log("myCharges" + turnCharges);
    }
    public void BasicAttack(GameObject attackTarget, string part)
    {
        attackTarget.GetComponent<IDamageable>().Damage(attack, part);
        myStatCard.UpdateTurnCharges();
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
    public int GetTurnCharges()
    {
        return turnCharges;
    }
    public void UseCharges(int value)
    {
        turnCharges-=value;
    }
    public void RefillCharges(int value)
    {
        turnCharges+=value;
    }
    public Vector2 GetPosition()
    {
        return myStats.myPosition;
    }

      public void Death()
    {
        //death animation
        Destroy(this.gameObject,0.5f);       
    }

}
