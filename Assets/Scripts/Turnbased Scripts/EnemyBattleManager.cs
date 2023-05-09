using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleManager : MonoBehaviour
{
    private static EnemyBattleManager instance;
    [SerializeField]
    EnemyAbstract[] enemies;
    [SerializeField]
    HeroAbstract[] playerHeroes;
    [SerializeField]
    List<EnemyAbstract> enemiesWithCharges;
    int attackIndex = 0;
    public bool enemySentToAttack = false;

    private bool enemyTurn = false;
    public static EnemyBattleManager Getinstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        GetAllEnemies();
        GetAllPlayerHeroes();
    }

   /*  public void StartEnemyTurn()
    {
        enemyTurn = true;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator EnemyTurn()
    {
        if(!enemySentToAttack && enemyTurn)
        {
            SendEnemyToAttack();
            enemySentToAttack = true;
        }
        yield return new WaitForSeconds(0.2f);

    } */
    public void SendEnemyToAttack()
    {
        GetAllEnemiesWithCharges();
        if(enemiesWithCharges.Count <= 0 )
        {
            BattleHandler.Getinstance().EndTurn();
            return;
        }

        BattleHandler.Getinstance().SetAttacker(enemiesWithCharges[0].gameObject);
        BattleHandler.Getinstance().SetTarget(DecideWhichPlayerToAttack());
        BattleHandler.Getinstance().LocalSlideToTarget("");

    }
    private void GetAllEnemies()
    {
        //enemies = GetComponentsInChildren<EnemyAbstract>();
        enemies = FindObjectsOfType<EnemyAbstract>();
        Array.Sort(enemies, (a, b) => a.myStats.dex.CompareTo(b.myStats.dex));
    }

    private void GetAllEnemiesWithCharges()
    {   enemiesWithCharges.Clear();
        foreach(EnemyAbstract enemy in enemies)
        {
            if(enemy.GetTurnCharges() > 0 && !enemiesWithCharges.Contains(enemy))
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
        if(playerHeroes.Length == 1)
        {
            return playerHeroes[0].gameObject;
        }
        foreach (HeroAbstract hero in playerHeroes)
        {
            if(hero.HP > lowestHp)
            {
                target = hero.gameObject;
                lowestHp = hero.HP;
            }
        }
        return target;
    }
}
