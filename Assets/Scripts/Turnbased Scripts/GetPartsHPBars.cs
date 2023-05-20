using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetPartsHPBars : MonoBehaviour
{
    [SerializeField]
    Transform contentHolder;
    [SerializeField]
    GameObject partHpTemplate, partMenu;

    EnemyAbstract target;

    MouseSelection mouseSelection;

    public void SpawnBars() //spawner is buggy fix where it is called and make sure there is no duplicates
    {
        target = BattleHandler.Getinstance().GetTarget().GetComponent<EnemyAbstract>();
        if(contentHolder.childCount > 0)
        {
            ClearBars();
        }
        if(target)
        {
            foreach(var part in target.myParts)
            {
                int partIndex = target.myParts.Where(x=> x.partName == part.partName).Select(x => target.myParts.IndexOf(x)).FirstOrDefault();
                if(target.myPartsHp[partIndex] > 0)
                {
                    var temp = Instantiate(partHpTemplate, contentHolder);
                    temp.GetComponent<PartBarManager>().PartBarInit(part.maxHP, target.myPartsHp[partIndex], target.myPartsArmor[partIndex], part.partName);
                }
            }
            partMenu.SetActive(true);
        } 
    }

    public void UpdateBars()
    {
        target = BattleHandler.Getinstance().GetTarget().GetComponent<EnemyAbstract>();
       
        if(target)
        {
            foreach(var part in target.myParts)
            {
                int partIndex = target.myParts.Where(x=> x.partName == part.partName).Select(x => target.myParts.IndexOf(x)).FirstOrDefault();
                if(target.myPartsHp[partIndex] > 0)
                {
                    var temp = Instantiate(partHpTemplate, contentHolder);
                    temp.GetComponent<PartBarManager>().PartBarInit(part.maxHP, target.myPartsHp[partIndex], target.myPartsArmor[partIndex], part.partName);
                }
            }
        } 
    }

    public void ClearBars()
    {
        foreach(Transform bar in contentHolder.transform)
        {
            GameObject.Destroy(bar.gameObject);
        }
    }
}