using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartEventTutorial()
    {
        Time.timeScale = 0f;
    }

    public void UnderstoodTutorial()
    {
        Time.timeScale = 1f;
    }
}
