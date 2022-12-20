using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    public class DialogueNodeMiniGame : DialogueNode
    {
        #region 
        [SerializeField]
        private bool isPlayerSpeaking = false;
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
   
       
        
        #endregion

    }
}
