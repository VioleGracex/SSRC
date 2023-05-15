using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Dialogue;

public class ClosingCircleQuickTimeEvent : MonoBehaviour
{
    [SerializeField] GameObject targetCircle;
    [SerializeField] float closingSpeed,forgivingThreshold;

    [SerializeField]
    GameObject diaObj;
    [SerializeField]
    GameObject diaCanvas;
    [SerializeField]
    GameObject closingCircleCanvas;
    
    private void Awake()
    {
       StartCoroutine(CloseCircle());
    }
    private IEnumerator CloseCircle()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while(this.transform.localScale.x > 0)
        {
            this.transform.localScale -= Vector3.one* closingSpeed* Time.deltaTime;
            yield return wait;
        }
        Debug.Log("FailedMiniGame");
        CallFailOnQuickTimeEvent();
    }
    public void QuickTimeEvent()
    {
        if(Mathf.Abs(this.transform.localScale.x - targetCircle.transform.localScale.x) <= forgivingThreshold )
        {
            CallSucceedOnQuickTimeEvent();

        }
        else
        {
            CallFailOnQuickTimeEvent();
        }
        closingCircleCanvas.SetActive(false);
    }

    private void CallSucceedOnQuickTimeEvent()
    {
        Debug.Log("succeed in minigame");
        PlayerPrefs.SetInt("PlayerInitiative", 1);
        //on load scene destroy dia system or turn it off or turn off canvas
        diaCanvas.SetActive(false);
        LoadFightScene();

    }

    private static void LoadFightScene()
    {
        /* //also create quick save to load on death before the mini game or after
        SceneManager.LoadSceneAsync("Prologue_TurnBased", LoadSceneMode.Additive);
        //loading screen wait set active unload
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(FindObjectOfType<PlayerConversant>().currentNode.GetFightSceneName()));
        SceneManager.UnloadSceneAsync("DialogueTestTemplate"); */
        //FindObjectOfType<LoadingScreen>().LoadNextScene(SceneManager.GetActiveScene().buildIndex+1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1 );    
        LoadingScreen.Getinstance().LoadNextScene();
    }

    private void CallFailOnQuickTimeEvent()
    {
        Debug.Log("FailedMiniGame");
        PlayerPrefs.SetInt("PlayerInitiative", 0);
        //on load scene destroy dia system or turn it off or turn off canvas
        diaCanvas.SetActive(false);
        LoadFightScene();
    }
}

