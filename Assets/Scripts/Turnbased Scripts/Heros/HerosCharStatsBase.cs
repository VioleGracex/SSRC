using UnityEngine;

[CreateAssetMenu(fileName = "HeroCharStat 1", menuName = "Char/Hero/HeroStats", order = 1)]
public class HerosCharStatsBase : ScriptableObject
{
    public string unitName;
    public float attack,defense,dex,maxHP,HP,exp,expToLvlUp;

    public int turnCharges,turnCharges_Max,mapLocation,level,levelCap,weaponMastry;

    public bool exhausted;

    public Vector3 myPosition;

}
