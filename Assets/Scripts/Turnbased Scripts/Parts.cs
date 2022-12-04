using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharParts_01", menuName = "Char/parts", order = 1)]
public class Parts : ScriptableObject
{
   
        public string partName,armorType;
        public int hp,armorHp;
        
        public Parts(string pName,string armorT, int health ,int hpArmor)
        {
            name=pName;
            armorType=armorT;
            hp = health;
            armorHp = hpArmor;
        }
 
}
