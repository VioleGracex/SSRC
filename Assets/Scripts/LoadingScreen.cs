using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    GameObject loadingBG;
    [SerializeField]
    Slider loadingSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadNextScene(int sceneIndex)
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
            }
            yield return null;
        }
    }
}
