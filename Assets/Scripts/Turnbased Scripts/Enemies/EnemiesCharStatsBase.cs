using UnityEngine;
using UnityEditor;
 using System;
 using System.Collections;
 using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyCharStat 1", menuName = "Char/Enemy/EnemyStats", order = 1)]
public class EnemiesCharStatsBase : ScriptableObject
{
    public string unitName;
    public float attack,defense,dex,maxHP;

    public int turnCharges_Max,level;
    
    public readonly string[] core_type =  {"green","red","violet"};

    [SerializeField,Header ("green,red,violet")]
    public int _coreTypeIndex;

    public string coreLocation;
    public Vector3 myPosition;

    [System.Serializable]
    public struct PartData
    {
       public string partName,armorType ;
       public float hp,armorHp,partDamageRate; //max rate is 100 (0-100) DamageRate
       
    }

    public List<PartData> partsData;

    


}
