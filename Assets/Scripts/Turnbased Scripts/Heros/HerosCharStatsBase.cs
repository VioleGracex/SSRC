using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HeroCharStat 1", menuName = "Char/Hero/HeroStats", order = 1)]
public class HerosCharStatsBase : ScriptableObject
{
    public string unitName;
    public float attack, defense, dex, maxHP, maxSP,expToLvlUp;

    public int turnCharges_Max,level,levelCap,weaponMastry;

    public bool exhausted;

    public Vector3 myPosition;
    public Sprite cardSprite;

    public GameObject charToken;

}
