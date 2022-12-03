using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class Json_SaveFile : MonoBehaviour
{
    public string FileName = string.Empty;
    public string FileName2 = string.Empty;
    public string FileName3 = string.Empty;
    public JsonSave ObjectData1 = new JsonSave();
    public JsonSave ObjectData2 = new JsonSave();
    public JsonSave ObjectData3 = new JsonSave();
    /* [System.Serializable]
    public struct NPC
    {
        public string name;
        public string affinity;
    } */

    
    [System.Serializable]
    public class JsonSave
    {
        public int savedScene;
        public string nodeName;
        public string backgroundName;
        public string location1_Name;
        public string location2_Name;
        public string location3_Name;
        //public List<NPC> savedAffinityList = new List<NPC>();
    }

    public void DeleteSlotData(int slotnumber)
    {
        if(slotnumber == 1)
        {
            ObjectData1 = new JsonSave();
            System.IO.File.WriteAllText (FileName, JsonUtility.ToJson (ObjectData1));
        }
        else if(slotnumber == 2)
        {
            ObjectData2 = new JsonSave();
            System.IO.File.WriteAllText (FileName2, JsonUtility.ToJson (ObjectData2));
        }
        else
        {
            ObjectData3= new JsonSave();
            System.IO.File.WriteAllText (FileName3, JsonUtility.ToJson (ObjectData3));
        }  
    }

    public void SaveToJson( int slotNumber)
    {
        Scene activeScene= SceneManager.GetActiveScene();
        //JsonSave JS = new JsonSave();
        GameObject[] GOs = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[];

        foreach(GameObject GO in GOs)
        {
            if(slotNumber == 1)
            {
                SaveGO(GO,ObjectData1);
            }
            else if(slotNumber == 2)
            {
                SaveGO(GO,ObjectData2);
            }
            else
            {
                SaveGO(GO,ObjectData3);
            }
            

        }
        if(slotNumber == 1)
        {
            ObjectData1.savedScene = activeScene.buildIndex;
            System.IO.File.WriteAllText (FileName, JsonUtility.ToJson (ObjectData1));
        }
        else if(slotNumber == 2)
        {
            ObjectData2.savedScene = activeScene.buildIndex;
            System.IO.File.WriteAllText (FileName2, JsonUtility.ToJson (ObjectData2));
        }
        else
        {
            ObjectData3.savedScene = activeScene.buildIndex;
            System.IO.File.WriteAllText (FileName3, JsonUtility.ToJson (ObjectData3));
        }

        
        // instantiate save slot
        //  var newPrefab = Instantiate (prefeb , pos) newprefab.name saveslot+"Counter"
    }

    private void SaveGO(GameObject GO, JsonSave data)
    {
        data.nodeName = GameObject.FindGameObjectWithTag("DialogueHolder").GetComponent<PlayerConversant>().currentNode.GetName();
        //ObjectData1.savedAffinityList=GameObject.FindGameObjectWithTag("DialogueHolder").GetComponent<PlayerConversant>().affinityList;
        if (GO.name == "background")
        {
            data.backgroundName = GO.GetComponent<Image>().sprite.name;
            //data.backgroundName = GameObject.FindGameObjectWithTag("BackGround").GetComponent<Image>().sprite.name;
            Debug.Log( data.backgroundName);
        }
        else if (GO.name == "left")
        {
            if (GO.GetComponent<Image>().sprite != null)
                data.location1_Name = GO.GetComponent<Image>().sprite.name;
            Debug.Log(GO.GetComponent<Image>().sprite.name);
        }
        else if (GO.name == "middle")
        {
            if (GO.GetComponent<Image>().sprite != null)
                data.location2_Name = GO.GetComponent<Image>().sprite.name;
            Debug.Log(GO.GetComponent<Image>().sprite.name);
        }
        else if (GO.name == "right")
        {
            if (GO.GetComponent<Image>().sprite != null)
                data.location3_Name = GO.GetComponent<Image>().sprite.name;
        }
    }

    public void LoadFromJson()
    {
        Debug.Log("loading");
        ObjectData1 = JsonUtility.FromJson<JsonSave>(File.ReadAllText (FileName));
        ObjectData2 = JsonUtility.FromJson<JsonSave>(File.ReadAllText (FileName));
        ObjectData3 = JsonUtility.FromJson<JsonSave>(File.ReadAllText (FileName));
        
        GameObject[] GOs = GameObject.FindObjectsOfType(typeof (GameObject)) as GameObject[];

        if (PlayerPrefs.GetInt("Isloading") == 1)
            {
                if(ObjectData1 != null)
                {
                    foreach(GameObject GO in GOs)
                    {  
                        if(GO.name == "background")
                        { 
                        Sprite temp = Resources.Load <Sprite> ("Backgrounds/"+ObjectData1.backgroundName);
                        GO.GetComponent<Image>().sprite = temp;
                        Debug.Log(temp.name);
                        }
                        else if( GO.tag == "PositionList")
                        {   
                            Sprite temp = Resources.Load <Sprite> ("Characters/"+ObjectData1.location1_Name);
                            if(temp != null)
                            {
                                GO.transform.GetChild(0).gameObject.SetActive(true);
                                GO.transform.GetChild (0).GetComponent<Image>().sprite = temp; // left 
                            }
                            temp = Resources.Load <Sprite> ("Characters/"+ObjectData1.location2_Name);
                             if(temp != null)
                            {
                                GO.transform.GetChild(1).gameObject.SetActive(true);
                                 GO.transform.GetChild(1).GetComponent<Image>().sprite = temp; // middle
                            }
                            temp = Resources.Load <Sprite> ("Characters/"+ObjectData1.location3_Name);
                             if(temp != null)
                            {
                                GO.transform.GetChild(2).gameObject.SetActive(true);
                                GO.transform.GetChild(2).GetComponent<Image>().sprite = temp; // right 
                            } 
                        } 
                    }
                }
            }
            else if (PlayerPrefs.GetInt("Isloading") == 2)
            {
               if(ObjectData2 != null)
                {
                    foreach(GameObject GO in GOs)
                    {  
                        if(GO.name == "background")
                        { 
                        Sprite temp = Resources.Load <Sprite> ("Backgrounds/"+ObjectData2.backgroundName);
                        GO.GetComponent<Image>().sprite = temp;
                        Debug.Log(temp.name);
                        }
                        else if( GO.tag == "PositionList")
                        {   
                            Sprite temp = Resources.Load <Sprite> ("Characters/"+ObjectData2.location1_Name);
                            if(temp != null)
                            {
                                GO.transform.GetChild(0).gameObject.SetActive(true);
                                GO.transform.GetChild (0).GetComponent<Image>().sprite = temp; // left 
                            }
                            temp = Resources.Load <Sprite> ("Characters/"+ObjectData2.location2_Name);
                             if(temp != null)
                            {
                                GO.transform.GetChild(1).gameObject.SetActive(true);
                                 GO.transform.GetChild(1).GetComponent<Image>().sprite = temp; // middle
                            }
                            temp = Resources.Load <Sprite> ("Characters/"+ObjectData2.location3_Name);
                             if(temp != null)
                            {
                                GO.transform.GetChild(2).gameObject.SetActive(true);
                                GO.transform.GetChild(2).GetComponent<Image>().sprite = temp; // right 
                            }   
                        }   
                    }
                }
            }
            else if (PlayerPrefs.GetInt("Isloading") == 3)
            {
              if(ObjectData3 != null)
                {
                    foreach(GameObject GO in GOs)
                    {  
                        if(GO.name == "background")
                        { 
                            Sprite temp = Resources.Load <Sprite> ("Backgrounds/"+ObjectData3.backgroundName);
                            GO.GetComponent<Image>().sprite = temp;
                            Debug.Log(temp.name);
                        }
                        else if( GO.tag == "PositionList")
                        {   
                            Sprite temp = Resources.Load <Sprite> ("Characters/"+ObjectData3.location1_Name);
                            if(temp != null)
                            {
                                GO.transform.GetChild(0).gameObject.SetActive(true);
                                GO.transform.GetChild (0).GetComponent<Image>().sprite = temp; // left 
                            }
                            temp = Resources.Load <Sprite> ("Characters/"+ObjectData3.location2_Name);
                             if(temp != null)
                            {
                                GO.transform.GetChild(1).gameObject.SetActive(true);
                                 GO.transform.GetChild(1).GetComponent<Image>().sprite = temp; // middle
                            }
                            temp = Resources.Load <Sprite> ("Characters/"+ObjectData3.location3_Name);
                             if(temp != null)
                            {
                                GO.transform.GetChild(2).gameObject.SetActive(true);
                                GO.transform.GetChild(2).GetComponent<Image>().sprite = temp; // right 
                            }   
                        } 
                    }
                }
            }
    }

    public string LoadNodeFromJson()
    {   
        ObjectData1 = JsonUtility.FromJson<JsonSave>(File.ReadAllText (FileName));
        ObjectData2 = JsonUtility.FromJson<JsonSave>(File.ReadAllText (FileName2));
        ObjectData3 = JsonUtility.FromJson<JsonSave>(File.ReadAllText (FileName3));
        if (PlayerPrefs.GetInt("Isloading") == 1)
            {
                if(ObjectData1 != null)
                { 
                    return ObjectData1.nodeName;
                }  
            }
        else if (PlayerPrefs.GetInt("Isloading") == 2)
            {
               if(ObjectData2 != null)
                {
                    return ObjectData2.nodeName;
                }
            }
        else 
            {
              if(ObjectData3 != null)
                {
                    return ObjectData3.nodeName;
                }
            }
        return "null";
    }
    // Start is called before the first frame update
    void Start()
    {
        FileName = Application.persistentDataPath + "SaveGameSlot1.json";
        FileName2 = Application.persistentDataPath + "SaveGameSlot2.json";
        FileName3 = Application.persistentDataPath + "SaveGameSlot3.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
