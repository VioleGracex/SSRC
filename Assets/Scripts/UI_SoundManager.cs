using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SoundManager : MonoBehaviour
{
    GameObject current_go;
    [SerializeField]
    AudioClip button_click;
    [SerializeField]
    AudioSource mixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != current_go)
        {
            //play sound 
            mixer.PlayOneShot(button_click);
            current_go = EventSystem.current.currentSelectedGameObject;
        }
    }
}
