using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostFightMenu : MonoBehaviour
{
    LoadingScreen loadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen = LoadingScreen.Getinstance();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        loadingScreen.LoadMainMenu();
    }

    public void RestartScene()
    {
        loadingScreen.RestartScene();
    }

    
}
