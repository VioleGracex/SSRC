using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField]
        PlayerConversant DialogueHolder;
        [SerializeField]
        GameObject chatboxHolder;
        [SerializeField]
        public TextMeshProUGUI chatbox;
        [SerializeField]
        TextMeshProUGUI speakerbox;
        [SerializeField]
        Button nextButton;
        [SerializeField]
        Button nextMidButton;
        [SerializeField]
        Transform choiceRoot;
        [SerializeField]
        GameObject choicePrefab;
        [SerializeField]
        Rt other;

        [SerializeField]
        Transform positionList;

        GameObject pauseMenu;

        [SerializeField]
        GameObject chatLogPrefab;
        [SerializeField]
        GameObject logPage;

        [SerializeField]
        bool autoChat = false;

        void Start()
        {
            nextButton.onClick.AddListener(Next);
            nextMidButton.onClick.AddListener(Next);
            //logPage =  GameObject.FindGameObjectWithTag("ChatLogLayout");
            GameObject saveInfoTemp = GameObject.FindGameObjectWithTag("SaveInfo");
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
            //playerName = saveInfoTemp.GetComponent<Undest_Info>().player_Name;
            UpdateUI();
        }

        void Update()
        {
            Debug.Log(FindObjectOfType<PlayerConversant>().currentNode.GetFightSceneName());
            if (Input.GetKeyDown("space") && chatbox.IsActive())
            {
                // nextButton.onClick.Invoke();
                Next();
                /* 
                 if(other.Cr())
               {
                   other.RevealAll();
               }
               else
               {
                  Next();
                  //UpdateUI();
               } */
            }
            else if(Input.GetKeyDown("space") && !chatbox.IsActive() && nextMidButton.IsActive())
            {
                nextMidButton.onClick.Invoke();
            }
            if (!other.Cr() && autoChat && !DialogueHolder.ISChoosing())
            {
              
                //Invoke("Next",2f);
                Next();
            }
           
        }

        public void MakeChatAuto()
        {
            autoChat = !autoChat;
        }
        void Next()
        {
            if (other.Cr())
            {
                other.RevealAll();
            }
            else
            {
                DialogueHolder.Next();
                UpdateUI();
            }


        }

        public void UpdateUI()
        {
            foreach(Transform child in positionList)
            {
                
                if(child.GetComponent<Image>().sprite == null)
                {
                    child.GetComponent<Image>().color = new Color(0f,0f,0f,0f);
                }
            }

            //sepreate choice and updating texts don't update them if you chooosing they stay the same
            //nextButton.gameObject.SetActive(DialogueHolder.HasNext());
            choiceRoot.gameObject.SetActive(DialogueHolder.ISChoosing());
            DialogueHolder.GetBG();
            if (DialogueHolder.ISChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                if (DialogueHolder.GetText() != "")
                {
                    chatboxHolder.gameObject.SetActive(true);
                    string temp = DialogueHolder.GetText();
                    temp = temp.Replace("Main", PlayerPrefs.GetString("PlayerName", "Cel"));
                    chatbox.text = temp;

                    if (other.Cr() == false)
                    {
                        other.Reveal();
                    }
                    if (DialogueHolder.GetCharName() == "Main")
                    {
                        speakerbox.text = PlayerPrefs.GetString("PlayerName", "cel");
                    }
                    else
                    {
                        speakerbox.text = DialogueHolder.GetCharName();
                    }
                    pauseMenu.GetComponent<PauseMenu>().SetDataChatLog(speakerbox.text, chatbox.text);
                    PutLogs(speakerbox.text, chatbox.text);

                }
                Sprite tempSprite = DialogueHolder.HasSprite();
                DialogueHolder.PlayVoiceLine();
                if (tempSprite != null)
                {
                    foreach (Transform child in positionList)
                    {

                        if (child.name == DialogueHolder.SendPosition())
                        {

                            child.gameObject.SetActive(true);
                            Image m_Image = child.GetComponent<Image>();
                            if (!DialogueHolder.ISFadeCheck())
                            {
                                // StartCoroutine(FadeImage(false, m_Image));
                            }

                            new WaitForSeconds(0.1f);
                            m_Image.sprite = tempSprite;
                            m_Image.SetNativeSize();

                        }
                        if (DialogueHolder.GetClearAllSprites())
                        {
                            Image m_Image = child.GetComponent<Image>();
                            //m_Image.sprite = null;
                            StartCoroutine(FadeImage(true, m_Image));
                        }
                        ChangeSpeakerSpriteColor(child.GetComponent<Image>());
                    }

                }
                else
                {
                    foreach (Transform child in positionList)
                    {

                        if (child.name == DialogueHolder.SendPosition())
                        {

                            Image m_Image = child.GetComponent<Image>();
                            if (!DialogueHolder.ISFadeCheck())
                            {
                                StartCoroutine(FadeImage(false, m_Image));
                            }
                            //m_Image.gameObject.SetActive(false); 
                            child.gameObject.SetActive(false);

                        }
                        if (DialogueHolder.GetClearAllSprites())
                        {
                            Image m_Image = child.GetComponent<Image>();
                            //m_Image.sprite = null;
                            StartCoroutine(FadeImage(true, m_Image));

                        }
                        ChangeSpeakerSpriteColor(child.GetComponent<Image>());


                    }

                }
                if (DialogueHolder.GetText() == "" && DialogueHolder.GetCharName() == "")
                {
                    if (DialogueHolder.currentNode.GetBGName() != null)
                    {
                        chatboxHolder.gameObject.SetActive(false);
                        //here fix
                    }
                    else if( DialogueHolder.currentNode.IsMiniGame())
                    {
                        Debug.Log("miniGame");
                        Instantiate(DialogueHolder.currentNode.GetMiniGame(),new Vector3(988, 688, 0),Quaternion.identity);
                        //instantiate prefab
                    }
                    else if (DialogueHolder.HasNext())
                    {
                        DialogueHolder.Next();
                    }

                }
            }


        }
        private void ChangeSpeakerSpriteColor(Image image)
        {

            if (image.sprite != null)
            {
                string[] name = image.sprite.name.Split(char.Parse("_"));

                if (speakerbox.text == name[1])
                {
                    //speaker stats where u add color changes material change etc
                    image.color = new Color32(255, 255, 255, 255);
                }
                else
                {
                    //non-speaker stats where u add color changes material change etc
                    image.color = new Color32(150, 150, 150, 255);
                }
                
                //if u want to make all the characters normal color during narrative talks
                /*else if (speakerbox.text == "Narrative")
                {
                    image.color = new Color32(255, 255, 255, 255);
                }*/
            }
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in DialogueHolder.GetChoices())
            {
                GameObject choiceInstance = GameObject.Instantiate(choicePrefab, choiceRoot);
                choiceInstance.GetComponentInChildren<TextMeshProUGUI>().text = choice.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    DialogueHolder.SelectChoice(choice);
                    UpdateUI();
                });
            }
        }

        IEnumerator FadeImage(bool fadeAway, Image img)
        {

            // fade from opaque to transparent
            if (fadeAway)
            {
                // loop over 1 second backwards
                for (float i = 1f; i >= 0f; i -= Time.deltaTime)
                {
                    // set color with i as alpha
                    img.color = new Color(0.8f, 0.8f, 0.8f, i);
                    yield return new WaitForSeconds(0.001f);
                }
                yield return new WaitForSeconds(0.5f);
                //img.gameObject.SetActive(false);
            }
            // fade from transparent to opaque
            else
            {
                // loop over 1 second
                for (float i = 0f; i <= 0.7; i += Time.deltaTime)
                {
                    // set color with i as alpha
                    img.color = new Color(0.8f, 0.8f, 0.8f, i);

                }
                yield return new WaitForSeconds(0.1f);
                for (float i = 0.7f; i <= 1f; i += Time.deltaTime)
                {
                    // set color with i as alpha
                    img.color = new Color(0.8f, 0.8f, 0.8f, i);
                }
                yield return new WaitForSeconds(0.5f);
            }
            //yield return new WaitForSeconds(0.5f);
            img.sprite = null;
        }

        void PutLogs(string s, string t)
        {
            GameObject temp = Instantiate(chatLogPrefab, transform.position, transform.rotation);
            temp.transform.SetParent(logPage.transform);
            GameObject Speaker = temp.transform.GetChild(0).gameObject;
            Speaker.GetComponent<TextMeshProUGUI>().text = s;
            GameObject chatText = temp.transform.GetChild(1).gameObject;
            chatText.GetComponent<TextMeshProUGUI>().text = t;
        }
    }
}
