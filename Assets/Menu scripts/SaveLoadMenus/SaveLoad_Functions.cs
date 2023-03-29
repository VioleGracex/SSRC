using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveLoad_Functions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PlayerConversant DialogueHolder;

    [SerializeField]
    Dialogue currentDialogue;

    [SerializeField]
    public DialogueNode savedNode;

    [SerializeField]
    GameObject saveMenu;
    [SerializeField]
    GameObject loadMenu;
    [SerializeField] 
    Button confirmButton;

    /* void OnLevelWasLoaded(int level) 
    {
        if (level != 0)
        {
            DialogueHolder =  GameObject.FindGameObjectWithTag("DialogueHolder").GetComponent<PlayerConversant>();
        }           
    } */

   
    void OnEnable() 
    {
      SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() 
    {
      SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (scene.buildIndex != 0)
        {
            DialogueHolder =  GameObject.FindGameObjectWithTag("DialogueHolder").GetComponent<PlayerConversant>();
            currentDialogue = DialogueHolder.GetComponent<PlayerConversant>().currentDialogue;
           
        }   
    }
    public void SaveNode(int slot)
    {
        // save background img
        Scene activeScene= SceneManager.GetActiveScene();
        savedNode =DialogueHolder.GetComponent<PlayerConversant>().currentNode;

        switch (slot)
        {
            case 1:
            PlayerPrefs.SetString("SavedPosition1",savedNode.GetName());
            PlayerPrefs.SetInt("LevelNumber1",activeScene.buildIndex);
            break;
            case 2:
            PlayerPrefs.SetString("SavedPosition2",savedNode.GetName());
            PlayerPrefs.SetInt("LevelNumber2",activeScene.buildIndex);
            break;
            case 3:
            PlayerPrefs.SetString("SavedPosition3",savedNode.GetName());
            PlayerPrefs.SetInt("LevelNumber3",activeScene.buildIndex);
            break;
            default:
            PlayerPrefs.SetString("SavedPosition1",savedNode.GetName());
            PlayerPrefs.SetInt("LevelNumber1",activeScene.buildIndex);
            break;
        }
        //PlayerPrefs.Save();
    }

    public void LoadSaveScene(int slot)
    {
        /*  switch (slot)
        {
            case 1:
            PlayerPrefs.SetInt("Isloading1",1);
            break;
            case 2:
            PlayerPrefs.SetInt("Isloading2",1);
            break;
            case 3:
            PlayerPrefs.SetInt("Isloading3",1);
            break;
            default:
            PlayerPrefs.SetInt("Isloading1",1);
            break;
        } */
        PlayerPrefs.SetInt("Isloading",slot);
        Time.timeScale = 1f;
        //loadScene
        
       
        //onesceneloaded
        //set current node
       
        //DialogueHolder.GetComponent<PlayerConversant>().Next();
        //crossfade
        //DialogueHolder.GetComponent<PlayerConversant>().CrossFade();
    }

    public void ConfirmLoad()
    {
        //there might be a bug here
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber"+  PlayerPrefs.GetInt("Isloading"),0));
        //this.GetComponent<Json_SaveFile>().LoadFromJson();
    }
    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void EnableSaveMenu()
    {
        saveMenu.SetActive(true);
    }
    public void EnableLoadMenu()
    {
        loadMenu.SetActive(true);
    }

    void Start()
    {
        //confirmButton.onClick.AddListener(ConfirmLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
