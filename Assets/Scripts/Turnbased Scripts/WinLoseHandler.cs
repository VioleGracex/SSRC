using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseHandler : MonoBehaviour
{
    [SerializeField]
    List<HeroAbstract> aliveHeroes;
    [SerializeField]
    List<EnemyAbstract> aliveEnemies;

    [SerializeField]
    GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        InitAliveUnits();
    }

    void InitAliveUnits()
    {
        HeroAbstract[] playerHeroes = FindObjectsOfType<HeroAbstract>();
        EnemyAbstract[] enemies = FindObjectsOfType<EnemyAbstract>();
        foreach(HeroAbstract hero in playerHeroes)
        {
            aliveHeroes.Add(hero);
        }
        foreach(EnemyAbstract enemy in enemies)
        {
            aliveEnemies.Add(enemy);
        }
    }

    public void HeroUnitDied(HeroAbstract deadUnit)
    {
        BattleHandler.Getinstance().state = BattleHandler.State.Returning;
        if(deadUnit.myStats.unitName == "player")
        {
            //call lose condition stop game call lost menu
            Time.timeScale = 0f; // check for time errors here
            deathScreen.SetActive(true);
        }
        else
        {
            aliveHeroes.Remove(deadUnit);
            //Destroy(deadUnit.gameObject,0.5f);
            if(aliveHeroes.Count <= 0)
            {
                //call lose condition stop game call lost menu
                Time.timeScale = 0f; // check for time errors here
                deathScreen.SetActive(true);
            }
        }
    }
    public void EnemyUnitDied(EnemyAbstract deadUnit)
    {
        BattleHandler.Getinstance().state = BattleHandler.State.Returning;
        FindObjectOfType<MouseSelection>().ClearTargetLabel();
        aliveEnemies.Remove(deadUnit);
        Destroy(deadUnit.gameObject,0.5f);
        if(aliveEnemies.Count <= 0)
        {
            //call win condition go to next scene
            LoadingScreen.Getinstance().LoadNextScene();
        }
   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
