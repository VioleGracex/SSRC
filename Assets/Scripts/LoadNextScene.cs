using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public void NextSceneLoad()
    {
        /* PlayerPrefs.DeleteKey("SavedPosition"+ SceneManager.GetActiveScene());
        PlayerPrefs.DeleteKey("LevelNumber"+SceneManager.GetActiveScene());
        PlayerPrefs.SetInt("IsLoading",SceneManager.GetActiveScene().buildIndex);
        //GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<Json_SaveFile>().DeleteSlotData(1);
        //loading screen
        //SceneManager.LoadSceneAsync(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1 ); */
        FindObjectOfType<LoadingScreen>().LoadNextScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
