using UnityEngine;
using UnityEditor;
 using System;
 using System.Collections;
 using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyCharStat 1", menuName = "Char/Enemy/EnemyStats", order = 1)]
public class EnemiesCharStatsBase : ScriptableObject
{
    public string unitName, enemyBodyType; // enemy type humanoid animal tentacle etc changes the body target ui to fit the part count and shape
    public float attack, defense, dex, maxHP;

    public int turnCharges_Max, level;
    
    public readonly string[] core_type =  {"green","red","violet"};

    [SerializeField,Header ("green,red,violet")]
    public int _coreTypeIndex;

    public string coreLocation;
    public Vector3 myPosition;

    [System.Serializable,HideInInspector]
    public struct PartData
    {
       public string partName, armorType, affectedStat; //the stat it affects when part is destroyed
       public float maxHP, maxArmor, partDamageRate,statLossRate; //max rate is 100 (0-100) DamageRate ,statLossRate ,armor is 4 max
       
    }

    public List<PartData> partsData;

    


}
