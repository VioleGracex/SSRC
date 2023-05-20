using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using TMPro;

public class EnemyAbstract : MonoBehaviour,IDamageable,IHeal,IAttack,ICharges,IReturnTurnCharges,IReturnPosition
{
    public EnemiesCharStatsBase myStats;
    public List<EnemiesCharStatsBase.PartData> myParts;
    public List <float> myPartsHp;
    public List <float> myPartsArmor;
    public Animator myAnimator;
    
    public float attack, defense, dex, HP;
    public Vector3 myPosition;

    [SerializeField]
    private int turnCharges, level, mapLocation;
    [SerializeField]
    string myCorePart;

    public bool exhausted;
    void Start()
    {
        myPosition = this.transform.position;
        myParts = myStats.partsData;   
        InitializeEnemyStats();
    }

    void InitializeEnemyStats()
    {
        // add level modifier
        level = myStats.level;
        attack = myStats.attack + ((level-1)*0.5f);
        defense = myStats.defense + ((level-1)*0.5f);
        dex = myStats.dex + ((level-1)*0.5f);
        myCorePart = myStats.coreLocation;
        foreach(var part in myParts)
        {   
            myPartsHp.Add(part.maxHP);
            myPartsArmor.Add(part.maxArmor);
        }
        HP = myPartsHp.Sum() + ((level-1)*0.5f) ;
        //SP = myStats.maxSP + ((level-1)*0.5f);
    }
    public void Newturn()
    {
        exhausted = false;
        turnCharges = myStats.turnCharges_Max;     
    }
    public void BasicAttack(GameObject attackTarget, string part)
    {
        attackTarget.GetComponent<IDamageable>().Damage(myStats.attack, "");
    }

    public void Damage(float damage, string damagedPart) //take damage on armor or part render it unusable and apply perctenage of it depending on weakness to current hp
    {
       //example how to find body part index
       int partIndex = myParts.Where(x=> x.partName == damagedPart).Select(x => myParts.IndexOf(x)).FirstOrDefault();
       float leftoverDamage = 0;

        if(myPartsArmor[partIndex] > 0)
        {
            leftoverDamage = damage - myPartsArmor[partIndex];
            myPartsArmor[partIndex]-=damage;
            Debug.Log("damaged "+ damagedPart + myPartsArmor[partIndex]);
        }
        else if(myPartsArmor[partIndex] <= 0 )
        {
            CheckPartHPDamage(damage, damagedPart, partIndex);
        }
        if (leftoverDamage > 0)
        {
            CheckPartHPDamage(leftoverDamage, damagedPart, partIndex);
        }
        HP = myPartsHp.Sum() + ((level-1)*0.5f) ;
        if(HP <= 0 )
        {
            Death();
        }
    }

    private void CheckPartHPDamage(float damage, string damagedPart, int partIndex)
    {
        myPartsHp[partIndex] -= damage;
        if (myPartsHp[partIndex] <= 0 && damagedPart != myCorePart)
        {
            //myPartsHp.Remove(partIndex); // cant remove i spawn from default part list and still exists there
            //call stats reduction
            myPartsHp[partIndex] = 0f;
        }
        else if (myPartsHp[partIndex] <= 0 && damagedPart == myCorePart)
        {
            Debug.Log("AM SO FKIN DEAD MATE!");
            this.GetComponent<BoxCollider2D>().enabled = false;
            BattleHandler.Getinstance().SetTarget(null); // there might be an error when attacker starts to return to position 
            //core part has bean destroyed so has been the enemy
            Death();
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
            HP += heal;
        }
    }

    public void RestorePart() //heals a part of body 
    {

    }

    public void Death()
    {
        //death animation
        FindObjectOfType<WinLoseHandler>().EnemyUnitDied(this);     
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
        return myPosition;
    }

    public float ReturnPartHP(string partName)
    {
        int partIndex = myParts.Where(x=> x.partName == partName).Select(x => myParts.IndexOf(x)).FirstOrDefault();
        return myPartsHp[partIndex];//myParts[temp].currentHP;
    }

}
