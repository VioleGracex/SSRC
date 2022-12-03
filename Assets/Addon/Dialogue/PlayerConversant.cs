using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] public Dialogue currentDialogue;
        [SerializeField] AudioSource soundPlayer;
        [SerializeField] AudioMixerGroup voiceLinesMixer;
        [SerializeField] GameObject backgroundImg;

        public DialogueNode currentNode = null;

        bool iSChoosing = false;

        [SerializeField]
        GameObject endMenu;

        [SerializeField]
        AudioSource BGM;

        [SerializeField]
        Animator transition;
        GameObject audioValues;
        //nova was here
        // [SerializeField] float soundPlayerVolume = 0.2f;

        public List<NPC> affinityList = new List<NPC>();

        [System.Serializable]
        public struct NPC
        {
            public string name;
            public string affinity;
        }
        public void Awake()
        {
            Debug.Log(SceneManager.sceneCountInBuildSettings);
            CrossFade();
            if (PlayerPrefs.GetInt("Isloading") != 0)
            {
                //currentNode = currentDialogue.GetSavedNode(PlayerPrefs.GetString("SavedPosition1", currentDialogue.GetRootNode().GetName()));
                currentNode = currentDialogue.GetSavedNode(GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<Json_SaveFile>().LoadNodeFromJson());
                Debug.Log(currentNode.name);
                GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<Json_SaveFile>().LoadFromJson();
                PlayerPrefs.SetInt("Isloading", 0);
            }
            /*  else if (PlayerPrefs.GetInt("Isloading") == 2)
             {
                 currentNode = currentDialogue.GetSavedNode(PlayerPrefs.GetString("SavedPosition2", currentDialogue.GetRootNode().GetName()));
                 GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<Json_SaveFile>().LoadFromJson();
                 PlayerPrefs.SetInt("Isloading", 0);
             }
             else if (PlayerPrefs.GetInt("Isloading") == 3)
             {
                 currentNode = currentDialogue.GetSavedNode(PlayerPrefs.GetString("SavedPosition3", currentDialogue.GetRootNode().GetName()));
                 GameObject.FindGameObjectWithTag("SaveLoadMenu").GetComponent<Json_SaveFile>().LoadFromJson();
                 PlayerPrefs.SetInt("Isloading", 0);
             }   */
            else
            {
                currentNode = currentDialogue.GetRootNode();
            }
            // nova commented these lines
            // audioValues = GameObject.FindGameObjectWithTag("AudioMixer");
            //soundPlayerVolume = audioValues.gameObject.GetComponent<audiosavedata>().backgroundMusic;
        }


        public void SaveNode()
        {
            /* PlayerPrefs.DeleteKey("SavedPosition");
            PlayerPrefs.DeleteKey("LevelNumber");
            PlayerPrefs.DeleteKey("SaveSlot"); */
            PlayerPrefs.SetString("SavedPosition", currentNode.GetName());
            Scene activeScene = SceneManager.GetActiveScene();
            PlayerPrefs.SetInt("LevelNumber", activeScene.buildIndex);
            PlayerPrefs.SetInt("SaveSlot", 1);
            //PlayerPrefs.Save();
        }
        public void LoadSaveNode()
        {
            PlayerPrefs.SetInt("Isloading", 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber", 0));
            currentNode = currentDialogue.GetSavedNode(PlayerPrefs.GetString("SavedPosition", "none"));
            CrossFade();
        }

        public bool ISChoosing()
        {
            return iSChoosing;
        }

        public bool GetClearAllSprites()
        {
            return currentNode.ClearAllSprites();
        }

        public bool ISFadeCheck()
        {
            return currentNode.GetFadeCheck();
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }
            return currentNode.GetText();
        }
        public string GetCurrentNodeName()
        {
            return currentNode.GetName();
        }


        public void PlayVoiceLine()
        {
            if (currentNode.GetVLName() != null)
            {
                soundPlayer.outputAudioMixerGroup = voiceLinesMixer;
                soundPlayer.PlayOneShot(currentNode.GetVLName());
            }
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            //BGM.volume = 0.0f;
            //see conditional affinity here
            foreach (DialogueNode node in currentDialogue.GetAllChildren(currentNode))
            {
                if (node.GetConditionAffinity() != "")
                {
                    string[] temp = node.GetConditionAffinity().Split(':');

                    if (PlayerPrefs.GetInt(temp[0] + PlayerPrefs.GetInt("Isloading")) >= int.Parse(temp[1]))
                    {
                        yield return node;
                    }
                }
                else
                {
                    yield return node;
                }
            }
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            //here dont fix audio this is shit nova removed it
            //BGM.volume = PlayerPrefs.GetFloat("BGMVolume", 0.2f);
            currentNode = chosenNode;
            iSChoosing = false;
            Next();
        }

        public string GetCharName()
        {
            if (currentDialogue == null)
            {
                return "";
            }
            return currentNode.GetCharName();
        }

        public void GetBG()
        {
            if (currentNode.GetBGName() != null)
            {
                if (currentNode.GetTransitionCheck())
                {
                    CrossFade();
                }
                backgroundImg.GetComponent<Image>().sprite = currentNode.GetBGName();
            }
        }

        public void CrossFade()
        {
            transition.Play("Crossfade_End", -1, 0f);
            transition.SetTrigger("Start");
        }

        public void SetTransitionOff()
        {
            transition.gameObject.SetActive(false);
        }

        public void Next()
        {
            if (currentNode.GetAffinityPoints() != 0)
            {
                HasAffinityPoints(currentNode.GetAffinityCharacter(), currentNode.GetAffinityPoints());
            }
            if (this.HasNext())
            {
                DialogueNode[] children = currentDialogue.GetAllChildren(currentNode).ToArray();
                if (children.Count() > 1)
                {
                    iSChoosing = true;
                    return;
                }
                currentNode = children[0];
                soundPlayer.Stop();
            }
            else
            {
                //chapter ends here
                endMenu.SetActive(true);
                //loading screen
                if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    SceneManager.LoadSceneAsync(0);
                }
               
            }
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }

        public Sprite HasSprite()
        {
            if (currentNode.GetSpriteName() != null)
                return currentNode.GetSpriteName();
            else
            {
                return null;
            }
        }

        void HasAffinityPoints(string name, int affinity)
        {
            if (PlayerPrefs.GetInt("Isloading") == 1)
            {
                PlayerPrefs.SetInt(name + '1', PlayerPrefs.GetInt(name + '1') + affinity);
            }
            else if (PlayerPrefs.GetInt("Isloading") == 2)
            {
                PlayerPrefs.SetInt(name + '2', PlayerPrefs.GetInt(name + '2') + affinity);
            }
            else if (PlayerPrefs.GetInt("Isloading") == 3)
            {
                PlayerPrefs.SetInt(name + '3', PlayerPrefs.GetInt(name + '3') + affinity);
            }
            else
            {
                PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name) + affinity);
            }


            /* if(!affinityList.Contains(name,0))
            {
                affinityList.Add(name,affinity);
                Debug.Log("yesh");
            }
            else
            {
                affinityList[name]+=affinity;
            } */
        }

        public string SendPosition()
        {
            return currentNode.GetSpritePosition();
        }



    }
}