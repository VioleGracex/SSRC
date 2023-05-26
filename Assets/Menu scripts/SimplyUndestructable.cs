using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplyUndestructable : MonoBehaviour
{
    private static bool exists = false;

    void OnEnable()
    {
        // Check if multiple instances exist during Awake (optional)
        GameObject[] me = GameObject.FindGameObjectsWithTag(this.tag);
        if (me.Length > 1)
        {
            Debug.Log("Destroyed duplicate instance");
            DestroyImmediate(this.gameObject);
        }
    }

    void Start()
    {
        // Ensure only one instance persists across scene changes
        if (!exists)
        {
            exists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance already exists, destroy this one
            Debug.Log("Destroyed duplicate instance");
            Destroy(gameObject);
        }
    }

}
