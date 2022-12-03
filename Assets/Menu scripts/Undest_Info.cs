using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undest_Info : MonoBehaviour
{
    [SerializeField] public string player_Name = "cel";
    [SerializeField] GameObject optionsSettings;
    //holds info shared across scenes
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] me =GameObject.FindGameObjectsWithTag(this.tag);
        if( me.Length > 1)
        {
             DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
