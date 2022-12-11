using UnityEngine;

[CreateAssetMenu(fileName = "HeroCharStat_01", menuName = "Char/Hero/HeroStats", order = 1)]
public class HerosCharStatsBase : ScriptableObject
{
    public float attack,defense,dex,maxHP,HP,exp,expToLvlUp;

    public int turnCharges,turnCharges_Max,mapLocation,level,levelCap,weaponMastry;

    public bool exhausted;

    Vector3 myPosition;

}
