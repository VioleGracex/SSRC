using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroExample : MonoBehaviour
{
    [SerializeField]
    CharStatsBase charStatsBase;

     [SerializeField]
    public struct Parts
    {
        public string name,armorType;
        public int hp,armorHp;
        
        public Parts(string partName,string armorT, int health ,int hpArmor)
        {
            name=partName;
            armorType=armorT;
            hp = health;
            armorHp = hpArmor;
        }
    }
    [SerializeField]
    public Parts[] myParts;
    // Start is called before the first frame update
   
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
