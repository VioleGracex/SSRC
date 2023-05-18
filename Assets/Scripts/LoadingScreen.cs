using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen instance;
    public static LoadingScreen Getinstance()
    {
        return instance;
    }
    [SerializeField]
    GameObject loadingBG;
    [SerializeField]
    Slider loadingSlider;
    
    public void LoadMainMenu()
    {
        loadingBG.SetActive(true);
        StartCoroutine(AsynchronousLoad(0));
    }

    public void RestartScene()
    {
        loadingBG.SetActive(true);
        StartCoroutine(AsynchronousLoad(SceneManager.GetActiveScene().buildIndex));
    }
    
    public void LoadNextScene()
    {
        loadingBG.SetActive(true);
        StartCoroutine(AsynchronousLoad(SceneManager.GetActiveScene().buildIndex+1));
    }
    public void LoadNextSceneByIndex(int sceneIndex)
    {
        loadingBG.SetActive(true);
        StartCoroutine(AsynchronousLoad(sceneIndex));
    }
    IEnumerator AsynchronousLoad (int sceneIndex)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            Debug.Log("Loading progress: " + (progress * 100) + "%");
            // Loading completed
            if (operation.progress == 0.9f)
            {
                Debug.Log("90%");
                loadingBG.SetActive(false);
                Time.timeScale = 1f;
            }
            yield return null;
        }
    }
}
