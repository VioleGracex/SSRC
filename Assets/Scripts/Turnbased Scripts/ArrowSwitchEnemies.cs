using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowSwitchEnemies : MonoBehaviour
{
  List<GameObject> enemies = new List<GameObject>();
    void Awake()
    {
      enemies = this.GetComponentsInChildren<GameObject>().ToList();
         
    }

    // Update is called once per frame
   
    public void GetNextEnemy()
    {
        int index= enemies.IndexOf(BattleHandler.Getinstance().target);
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
        int index = enemies.IndexOf(BattleHandler.Getinstance().target);
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
