using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPartsHPBars : MonoBehaviour
{
    [SerializeField]
    Transform contentHolder;
    [SerializeField]
    GameObject partHpTemplate;
    public void SpawnBars(EnemyAbstract target)
    {
        foreach(var part in target.myParts)
        {
            var temp = Instantiate(partHpTemplate, contentHolder);
            temp.GetComponent<PartBarManager>().PartBarInit(part.maxHP, part.currentHP,part.currentArmor);
        }
    }
}
