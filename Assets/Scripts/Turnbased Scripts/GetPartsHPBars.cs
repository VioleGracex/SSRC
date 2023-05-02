using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPartsHPBars : MonoBehaviour
{
    [SerializeField]
    Transform contentHolder;
    [SerializeField]
    GameObject partHpTemplate, partMenu;

    EnemyAbstract target;

    MouseSelection mouseSelection;

    [SerializeField]
    BattleHandler battleHandler;
    public void SpawnBars()
    {
        target = battleHandler.target.GetComponent<EnemyAbstract>();
        if(target)
        {
            foreach(var part in target.myParts)
            {
                var temp = Instantiate(partHpTemplate, contentHolder);
                temp.GetComponent<PartBarManager>().PartBarInit(part.maxHP, part.currentHP,part.currentArmor, part.partName);
            }
            partMenu.SetActive(true);

        }
       
    }
}
