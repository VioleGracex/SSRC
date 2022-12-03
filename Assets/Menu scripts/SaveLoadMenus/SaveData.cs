using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public struct NPC
    {
        public string name;
        public string affinity;
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        // Update is called once per frame
        public void LoadJson()
        {
            
        }
    }
    // Start is called before the first frame update
    public interface ISaveable
    {
        //void PopulateSaveData{};
    }
    
}
