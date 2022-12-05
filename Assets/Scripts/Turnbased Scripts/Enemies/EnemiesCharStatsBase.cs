using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCharStat_01", menuName = "Char/Enemy/EnemyStats", order = 1)]
public class EnemiesCharStatsBase : ScriptableObject
{
    public float attack,defense,dex,maxHP,HP;

    public int turnCharges,turnCharges_Max,mapLocation;

    public bool exhausted;

    public readonly string[] core_type =  {"green","red","violet"};

    [SerializeField,Header ("green,red,violet")]
    public int _coreTypeIndex;

    public string coreLocation;
    public Vector3 myPosition;

    [System.Serializable]
    public struct PartData
    {
       public string partName,armorType;
       public int hp,armorHp;
    }

    public PartData[] partsData;

}
