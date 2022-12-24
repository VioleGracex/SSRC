using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using TMPro;

public class EnemyAbstract : MonoBehaviour,IDamageable,IHeal,IAttack,ICharges,IReturnTurnCharges,IReturnPosition
{
    public EnemiesCharStatsBase myStats;
    public List<EnemiesCharStatsBase.PartData> myParts;
    public Animator myAnimator;
    
     public float attack,defense,dex,HP;

    public int turnCharges,mapLocation;

    public bool exhausted;
    
    void Start()
    {
        myStats.myPosition = this.transform.position;
        myParts = myStats.partsData;
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
        attackTarget.GetComponent<IDamageable>().Damage(myStats.attack);
    }

    public void Damage(float damage) //take damage on armor or part render it unusable and apply perctenage of it depending on weakness to current hp
    {
       //get battlehandler selected part here
       //example how to find body part index
       string damagedPart = GameObject.Find("PartName").GetComponent<TextMeshProUGUI>().text;
       int temp = myParts.Where(x=> x.partName == damagedPart).Select(x => myParts.IndexOf(x)).FirstOrDefault();
       EnemiesCharStatsBase.PartData tempPart = myParts[temp];

        if(tempPart.armorHp > 0)
        {
           tempPart.armorHp-=damage;
           Debug.Log("damaged");
        }
        else if(tempPart.hp > 0 )
        {
           tempPart.hp -= damage * (tempPart.partDamageRate/100);
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
    public int ReturnCharges()
    {
        return turnCharges;
    }
    public Vector2 ReturnPosition()
    {
        return myStats.myPosition;
    }

}
