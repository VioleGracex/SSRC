using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAChild : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject childOfMe;
    public void EnableMyChild()
    {
        childOfMe.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
