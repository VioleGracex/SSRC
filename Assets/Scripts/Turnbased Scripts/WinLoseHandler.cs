using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseHandler : MonoBehaviour
{
    List<HeroAbstract> aliveHeroes;
    List<EnemyAbstract> aliveEnemies;

    [SerializeField]
    GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("AMHERE");
        if(deadUnit.myStats.unitName == "player")
        {
            //call lose condition stop game call lost menu
            Time.timeScale = 0f; // check for time errors here
            deathScreen.SetActive(true);
        }
        else
        {
            Debug.Log(aliveHeroes.Contains(deadUnit));
            //aliveHeroes.Remove(deadUnit);
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
