using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "CharStat_01", menuName = "Char/EnemyStats", order = 1)]
public class CharStatsBase : ScriptableObject
{
    public float attack,defense,dex,maxHP,HP;
    public readonly string[] attack_type =  {"slash","bludge","pierce"};

    [SerializeField,Header ("slash,bludge,pierce")]
    public int _atkTypeIndex;

    public int turnCharges,turnCharges_Max,mapLocation;

    bool exhausted;

    public readonly string[] core_type =  {"green","red","violet"};

    [SerializeField,Header ("green,red,violet")]
    public int _coreTypeIndex;

    public string coreLocation;
    /*public string[] bodyParts,armor_; */
   
    public Parts[] myParts;


}
