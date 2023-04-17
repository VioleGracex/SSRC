using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOnTarget : MonoBehaviour
{
    [SerializeField]
    Transform target,myUI;
    [SerializeField] 
    Vector3 targetOffset;   

    // Update is called once per frame
    public void LocateTargetFollow()
    {
        myUI.position = Camera.main.WorldToViewportPoint(target.position); 
        myUI.gameObject.SetActive(true);
    }
}
