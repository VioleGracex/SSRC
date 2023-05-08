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

    [SerializeField]
    BattleHandler battleHandler;
    public void SpawnBars()
    {
        target = battleHandler.GetTarget().GetComponent<EnemyAbstract>();
       
        if(target)
        {
            foreach(var part in target.myParts)
            {
                var temp = Instantiate(partHpTemplate, contentHolder);
                int partIndex = target.myParts.Where(x=> x.partName == part.partName).Select(x => target.myParts.IndexOf(x)).FirstOrDefault();
                temp.GetComponent<PartBarManager>().PartBarInit(part.maxHP, target.myPartsHp[partIndex], target.myPartsArmor[partIndex], part.partName);
            }
            partMenu.SetActive(true);
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
