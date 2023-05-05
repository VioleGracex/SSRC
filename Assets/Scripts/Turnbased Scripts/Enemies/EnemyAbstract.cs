using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using TMPro;

public class EnemyAbstract : MonoBehaviour,IDamageable,IHeal,IAttack,ICharges,IReturnTurnCharges,IReturnPosition
{
    public EnemiesCharStatsBase myStats;
    public List<EnemiesCharStatsBase.PartData> myParts;
    public List <float> myPartsArmor;
    public Animator myAnimator;
    
    public float attack, defense, dex, HP;

    private int turnCharges, level, mapLocation;

    public bool exhausted;
    void Start()
    {
        myStats.myPosition = this.transform.position;
        myParts = myStats.partsData;   
    }

    void InitializeEnemyStats()
    {
        // add level modifier
        level = myStats.level;
        attack = myStats.attack + ((level-1)*0.5f);
        defense = myStats.defense + ((level-1)*0.5f);
        dex = myStats.dex + ((level-1)*0.5f);
        HP = myStats.maxHP + ((level-1)*0.5f) ;
        foreach(var part in myParts)
        {
            myPartsArmor.Add(part.maxArmor);
        }
        //SP = myStats.maxSP + ((level-1)*0.5f);
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
    public void Attack(GameObject attackTarget, string part)
    {
        attackTarget.GetComponent<IDamageable>().Damage(myStats.attack, "");
    }

    public void Damage(float damage, string damagedPart) //take damage on armor or part render it unusable and apply perctenage of it depending on weakness to current hp
    {
       //get battlehandler selected part here
       //example how to find body part index
       int temp = myParts.Where(x=> x.partName == damagedPart).Select(x => myParts.IndexOf(x)).FirstOrDefault();
       
       //EnemiesCharStatsBase.PartData tempPart = myParts[temp];

        if(myPartsArmor[temp] > 0)
        {
           myPartsArmor[temp]-=damage;
           Debug.Log("damaged"+ damagedPart);
        }
        else if(myPartsArmor[temp] > 0 )
        {
           myPartsArmor[temp] -= damage * (myParts[temp].partDamageRate/100);
        }
    }
    public void Heal(float heal) //heals main hp
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

    public void RestorePart() //heals a part of body 
    {

    }

    public void Death()
    {
        //death animation
        Destroy(this.gameObject,0.5f);       
    }

    public void Charges(int usage)
    {
        turnCharges -= usage;
    }
    public int GetTurnCharges()
    {
        return turnCharges;
    }
    public Vector2 GetPosition()
    {
        return myStats.myPosition;
    }

    public float ReturnPartHP(string partName)
    {
        int temp = myParts.Where(x=> x.partName == partName).Select(x => myParts.IndexOf(x)).FirstOrDefault();
        return 0;//myParts[temp].currentHP;
    }

}
