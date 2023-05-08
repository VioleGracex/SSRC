using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowSwitchEnemies : MonoBehaviour
{
   [SerializeField]
    List<GameObject> enemies ;
    void Awake()
    {
       // Transform[] temp = this.GetComponentsInChildren<Transform>();
        foreach (Transform item in this.transform)
        {
            enemies.Add(item.gameObject);
        }
         
    }

    // Update is called once per frame
   
    public void GetNextEnemy()
    {
        int index= enemies.IndexOf(BattleHandler.Getinstance().GetTarget());
        if(index == enemies.Count-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        BattleHandler.Getinstance().SetTarget(enemies[index]);
    }

    public void GetPreviousEnemy()
    {
        int index = enemies.IndexOf(BattleHandler.Getinstance().GetTarget());
        if (index == 0)
        {
            index = enemies.Count-1;
        }
        else
        {
            index--;
        }
        BattleHandler.Getinstance().SetTarget(enemies[index]);
    }
}
