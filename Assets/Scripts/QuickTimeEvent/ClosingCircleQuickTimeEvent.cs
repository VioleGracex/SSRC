using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingCircleQuickTimeEvent : MonoBehaviour
{
    [SerializeField] GameObject targetCircle;
    [SerializeField] float closingSpeed,forgivingThreshold;
    
    private void Awake()
    {
       StartCoroutine(CloseCircle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator CloseCircle()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while(this.transform.localScale.x > 0)
        {
            this.transform.localScale -=Vector3.one* closingSpeed* Time.deltaTime;
            yield return wait;
        }
        Debug.Log("FailedMiniGame");
    }
    public void QuickTimeEvent()
    {
        if(Mathf.Abs(this.transform.localScale.x - targetCircle.transform.localScale.x) <= forgivingThreshold )
        {
            CallSuccedOnQuickTimeEvent();

        }
        else
        {
            CallFailOnQuickTimeEvent();
        }
    }

    private void CallSuccedOnQuickTimeEvent()
    {
        Debug.Log("Succed in minigame");
    }
    private void CallFailOnQuickTimeEvent()
    {
        Debug.Log("FailedMiniGame");
    }
}
