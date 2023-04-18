using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetSpeaker : MonoBehaviour
{
    TextMeshProUGUI myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = this.GetComponent<TextMeshProUGUI>();
        //myText.text = PixelCrushers.DialogueSystem.DialogueLua.GetVariable("Speaker").asString;
        UpdateSpeaker();

    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
         //Your Function You Want to Call
        myText.text = PixelCrushers.DialogueSystem.DialogueLua.GetVariable("Speaker").asString;
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateSpeaker()
    {
        myText.text = PixelCrushers.DialogueSystem.DialogueLua.GetVariable("Speaker").asString;
    }
    public void SetSpeaker(string speaker)
    {
        myText.text = speaker;
    }
}
