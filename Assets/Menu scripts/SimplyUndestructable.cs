using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplyUndestructable : MonoBehaviour
{
    private static bool exists = false;
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
