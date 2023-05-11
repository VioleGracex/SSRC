using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroupText : MonoBehaviour
{
    [SerializeField]
    List<TextMeshProUGUI> grouptext;

    [SerializeField]
    bool initNames;

    [SerializeField]
    Sprite activeBtn, disabledBtn;
    [SerializeField]
    GameObject currentActive = null;

    Color bluish;
    void Start()
    {
        
        if(initNames)
        {
            foreach(TextMeshProUGUI txt in grouptext)
            {
                txt.color = Color.grey;
                txt.transform.parent.GetComponent<Image>().sprite = disabledBtn;
            }
        } 
    }

    public void ActiveMe(GameObject active)
    {
        if(currentActive == null)
        {
            active.GetComponent<Image>().sprite = activeBtn;
            //change color to white
            active.GetComponentInChildren<TextMeshProUGUI>().color = Color.grey;
            currentActive = active;
            return;
        }
        else if(active == currentActive)
        {
            //change color to blue
            active.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
            active.GetComponent<Image>().sprite = disabledBtn;
            currentActive = null;
            return;
        }
        currentActive.GetComponent<Image>().sprite = disabledBtn;
         //change color to blue
        active.GetComponent<Image>().sprite = activeBtn;
        //change color to white
        currentActive = active;
        
       /*  foreach(var txt in grouptext)
            {
                if(active != txt.transform.parent.gameObject)
                {
                    //change color to blue
                    txt.transform.parent.GetComponent<Image>().sprite = disabledBtn;
                   
                }
                else
                {
                    //change color to white
                    txt.transform.parent.GetComponent<Image>().sprite = activeBtn;
                    currentActive = txt.transform.gameObject;
                }
                    
            } */
    }

}
