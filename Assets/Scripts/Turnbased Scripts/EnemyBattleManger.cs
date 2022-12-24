using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleManger : MonoBehaviour
{
    EnemyAbstract[] enemies;
    HeroAbstract[] playerHeroes;
    int attackIndex;
    // Start is called before the first frame update
    void Awake()
    {
        GetAllEnemies();
        GetAllPlayerHeroes();
    }

    private void SendEnemyToAttack()
    {
        if (enemies[attackIndex].gameObject == null)
        {
            if(attackIndex >= enemies.Length-1)
            {
                attackIndex = 0;
            }
            else
            {
                attackIndex++;
            }
        }
        BattleHandler.Getinstance().SlideToTargetFunction(enemies[attackIndex].gameObject, DecideWhichPlayerToAttack());
        if(attackIndex < enemies.Length-1)
        {
            attackIndex++;
        }
        else
        {
            attackIndex = 0;
        }
        
    }
    private void GetAllEnemies()
    {
        enemies = GetComponentsInChildren<EnemyAbstract>();
        Array.Sort(enemies, (a, b) => a.myStats.dex.CompareTo(b.myStats.dex));
    }

    private void GetAllPlayerHeroes()
    {
        playerHeroes = FindObjectsOfType<HeroAbstract>();
        
    }
    private GameObject DecideWhichPlayerToAttack()
    {
        float lowestHp =0;
        GameObject target = null;
        foreach (var item in playerHeroes)
        {
            if(item.HP > lowestHp)
            {
                target = item.gameObject;
                lowestHp = item.HP;
            }
            else
            {
                target = null;
            }
        }
        return target;
    }

  
}
