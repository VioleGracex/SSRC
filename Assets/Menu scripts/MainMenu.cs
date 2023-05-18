using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

    int lvlNumber;

    void Awake()
    {
        PlayerPrefs.SetInt("Isloading",0);
    }
    public void PlayGame()
	{
        SceneManager.LoadScene(lvlNumber);
	}

    public void FastNewGame()
    {
       LoadingScreen.Getinstance().LoadNextSceneByIndex(1);
    }
    /*public void ChangeToggle()
    {
        myToggle.isOn = !myToggle.isOn;
    }*/

    public void Togglename(int chosenlvl)
    {
        lvlNumber = chosenlvl;
    }

    public void ChosenNewSlot(int chosenSlot)
    {
        PlayerPrefs.DeleteKey("SavedPosition"+chosenSlot);
        PlayerPrefs.DeleteKey("LevelNumber"+chosenSlot);
        PlayerPrefs.SetInt("IsLoading",chosenSlot);
        GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<Json_SaveFile>().DeleteSlotData(chosenSlot);
        //loading screen
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
	{
		Application.Quit ();
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&SceneManager.GetActiveScene().buildIndex==0)
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
    }
}
