using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        #region 
        [SerializeField]
        private bool isPlayerSpeaking = false;
        [SerializeField]
        private bool isMiniGame = false;
        [SerializeField]
        private bool isSpriteVisible = false;
        [SerializeField]
        private bool isRootNode = false;
        [SerializeField]
        private string parentID;
        [SerializeField] 
        private string charName;
        [SerializeField] 
        private string text;
        [SerializeField] 
        private AudioClip voiceLine;
        [SerializeField] 
        private AudioClip musicAudio;
        [SerializeField] 
        private List<string> children = new List<string>();
        [SerializeField] 
        private Rect pos = new Rect (0, 0, 200, 200);
        [SerializeField] 
        private Sprite charSprite ;
        [SerializeField]
        private string spritePosition;
        [SerializeField]
        private bool clearOtherSprites;
        [SerializeField]
        private bool fadeCheck;
        [SerializeField] 
        private Sprite BG ; 
        [SerializeField]
        private bool BackGroundTransition;
        [SerializeField]
        private int nodeIndexPosition = 0;
        [SerializeField]
        private int nodeNumber;
        [SerializeField]
        private int affinityPoints;
        [SerializeField]
        private string affinityCharacter;
        [SerializeField]
        private string conditionalAffinity;
        [SerializeField]
        private string miniGameName;
        [SerializeField]
        private GameObject miniGame;
        [SerializeField]
        private string fightSceneName;
        
    #endregion


        public Rect GetPos()
        {
            return pos;
        }

        public string GetText()
        {
            return text;
        }

        public string GetParentName()
        {
            return parentID;
        }

        public string GetName()
        {
            return name;
        }

        public string GetCharName()
        {
            return charName;
        }
        public string GetConditionAffinity()
        {
            return conditionalAffinity;
        }
        public string GetAffinityCharacter()
        {
            return affinityCharacter;
        }
        public string GetSpritePosition()
        {
            return spritePosition;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public Sprite GetSpriteName()
        {
            return charSprite;
        }

        public Sprite GetBGName()
        {
            return BG;
        }

        //changed that into string
        public AudioClip GetVLName()
        {
            return voiceLine;
        }
        public AudioClip GetMusicName()
        {
            return voiceLine;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
        public bool IsMiniGame()
        {
            return isMiniGame;
        }

        public bool IsRootNode()
        {
            return isRootNode;
        }
        public bool IsSpriteVisible()
        {
            return isSpriteVisible;
        }
        public int GetIndexPosition()
        {
            return nodeIndexPosition;
        }

        public bool ClearAllSprites()
        {
            return clearOtherSprites;
        }

        public bool GetFadeCheck()
        {
            return fadeCheck;
        }
        public bool GetTransitionCheck()
        {
            return BackGroundTransition;
        }
        public int GetNodeNumber()
        {
            return nodeNumber;
        }
        
        public int GetAffinityPoints()
        {
            return affinityPoints;
        }

        public string GetMiniGameName()
        {
            return miniGameName;
        }

        public GameObject GetMiniGame()
        {
            return miniGame;
        }
        public string GetFightSceneName()
        {
            return fightSceneName;
        }
        
#if UNITY_EDITOR
        public void SetPos(Vector2 newPosition)
        {
            Undo.RecordObject(this ,"Move Dialogue");
            pos.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetParentName(string newParentID)
        {          
            Undo.RecordObject(this ,"Set Parent Name");
            parentID = newParentID;        
            EditorUtility.SetDirty(this);
        }


        public void SetCharName(string newSpeaker)
        {
            if(newSpeaker != charName)
            {
                Undo.RecordObject(this ,"Set CharName");
                charName = newSpeaker;
                EditorUtility.SetDirty(this);
            }               
        }

        public void SetText(string newText)
        {
            if(newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }         
        }

        public void SetSpritePosition(string newText)
        {
            if(newText != text)
            {
                Undo.RecordObject(this, "Update Position ");
                spritePosition = newText;
                EditorUtility.SetDirty(this);
            }         
        }
        public void SetIndexPosition(int newIndex)
        {
                Undo.RecordObject(this, "Update index pos");
                nodeIndexPosition = newIndex;
                EditorUtility.SetDirty(this);        
        }


        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }
        public void SetCharVisibilty()
        {
            Undo.RecordObject(this, "switched visibilty of char");
            isSpriteVisible = !isSpriteVisible; 
            EditorUtility.SetDirty(this);      
        }

         public void SetRootNode(bool rootvalue)
        {
            isRootNode = rootvalue;
            EditorUtility.SetDirty(this);   
        }
        public void SetNodeNumber(int prevNum)
        {
            nodeNumber = prevNum;
            EditorUtility.SetDirty(this);
        }
        public void SetAffinityPoints(int points)
        {
            affinityPoints = points;
            EditorUtility.SetDirty(this);
        }
        public void SetAffinityPoints(string affChar)
        {
            affinityCharacter = affChar;
            EditorUtility.SetDirty(this);
        }
        public void SetConditionalAffinity(string condtion)
        {
            conditionalAffinity = condtion;
            EditorUtility.SetDirty(this);
        }
        public void SetBG(Sprite backgroundSprite)
        {
            BG = backgroundSprite;
            EditorUtility.SetDirty(this);
        }
        public void SetMiniGameName(string mGName)
        {
            miniGameName = mGName;
            EditorUtility.SetDirty(this);
        }
        public void SetMiniGame(GameObject mG)
        {
            miniGame = mG;
            EditorUtility.SetDirty(this);
        }
        public void SetFightSceneName(string sceneName)
        {
            fightSceneName = sceneName;
            EditorUtility.SetDirty(this);
        }
#endif

    }
}
