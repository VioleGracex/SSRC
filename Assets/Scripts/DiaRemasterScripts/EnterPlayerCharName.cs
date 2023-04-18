using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PixelCrushers.DialogueSystem;

public class EnterPlayerCharName : MonoBehaviour
{
    [SerializeField]
    GameObject diary;
    [SerializeField]
    Button continueBtn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetNamePlayer(TMP_InputField inputName)
    {
        if(inputName.text != "")
        {   
            DialogueLua.SetActorField("Player", "Display Name", inputName.text);
            diary.SetActive(false);
            GameObject.FindObjectOfType<ContinueBtnsManager>().EnableAllBtns();
            continueBtn.onClick.Invoke();
        }
        else
        {
            Debug.Log("Invalid Name");
        }
       
    }
}
