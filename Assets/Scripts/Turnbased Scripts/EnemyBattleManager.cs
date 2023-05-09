using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleManager : MonoBehaviour
{
    EnemyAbstract[] enemies;
    HeroAbstract[] playerHeroes;
    List<EnemyAbstract> enemiesWithCharges;
    int attackIndex;
    // Start is called before the first frame update
    void Awake()
    {
        GetAllEnemies();
        GetAllPlayerHeroes();
    }

    private void SendEnemyToAttack()
    {
        GetAllEnemiesWithCharges();
        if(enemiesWithCharges.Count <= 0 )
        {
            BattleHandler.Getinstance().EndTurn();
            return;
        }
        if (enemiesWithCharges[attackIndex].gameObject == null)
        {
            if(attackIndex >= enemiesWithCharges.Count-1)
            {
                attackIndex = 0;
            }
            else
            {
                attackIndex++;
            }
        }

        BattleHandler.Getinstance().SetAttacker(enemies[attackIndex].gameObject);
        BattleHandler.Getinstance().SetTarget(DecideWhichPlayerToAttack());
        BattleHandler.Getinstance().LocalSlideToTarget("");
        
        if(attackIndex < enemiesWithCharges.Count-1)
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

    private void GetAllEnemiesWithCharges()
    {
        foreach( EnemyAbstract enemy in enemies)
        {
            if(enemy.GetTurnCharges() > 0)
                enemiesWithCharges.Add(enemy);
        }
    }
    private void GetAllPlayerHeroes()
    {
        playerHeroes = FindObjectsOfType<HeroAbstract>();
    }
    private GameObject DecideWhichPlayerToAttack()
    {
        float lowestHp = 0;
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
