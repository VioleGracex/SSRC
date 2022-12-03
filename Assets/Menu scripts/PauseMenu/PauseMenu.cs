using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance { get; private set; }
    [SerializeField] GameObject pauseMenu;
    
    int countChatLog = 0;

    bool spedUp=false;

    public class TwoStringArray
    {
        public string speaker;
        public string textContext;

        public TwoStringArray(string s , string t)
        {
            this.speaker = s;
            this.textContext = t;
        }

    }

    List <TwoStringArray> chatLog = new List<TwoStringArray>();

    public void SetDataChatLog(string s,string t)
    {
        chatLog.Add(new TwoStringArray(s,t));
        countChatLog++; 
    }

     void Start()
    {
        instance= this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeMenu();
        }
    }

    public void EscapeMenu()
    {
        if(GameObject.FindGameObjectWithTag("BackButton")!=null)
        {
            GameObject.FindGameObjectWithTag("BackButton").GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                if (pauseMenu.activeSelf)
                {
                    Time.timeScale = 0f;
                }
                else if(spedUp)
                {
                    Time.timeScale = 2f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            } 
        }
        
    }

    public void OpenOptionsMenu()
    {
        GameObject.FindGameObjectWithTag("OptionsMenu").GetComponent<EnableAChild>().EnableMyChild();
    }
    public void OpenSaveMenu()
    {
        GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<SaveLoad_Functions>().EnableSaveMenu();
    }
    public void OpenLoadMenu()
    {
        GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<SaveLoad_Functions>().EnableLoadMenu();
    }

    public void SpeedUpGame( )
    {
        spedUp = !spedUp;
        if(spedUp)
        {
            Time.timeScale = 2f;
        }
        else
         Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene (0);
        Time.timeScale = 1f;
    }
}
