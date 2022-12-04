using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCharStat_01", menuName = "Char/Enemy/EnemyStats", order = 1)]
public class EnemiesCharStatsBase : ScriptableObject
{
    public float attack,defense,dex,maxHP,HP;

    public int turnCharges,turnCharges_Max,mapLocation;

    bool exhausted;

    public readonly string[] core_type =  {"green","red","violet"};

    [SerializeField,Header ("green,red,violet")]
    public int _coreTypeIndex;

    public string coreLocation;
    /*public string[] bodyParts,armor_; */
   
    public Parts[] myParts;


}
