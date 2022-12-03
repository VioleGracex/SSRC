using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;
using System;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        
        Dialogue selectedDialogue = null;
        [NonSerialized]
        GUIStyle nodeStyle;
        [NonSerialized]
        GUIStyle playerNodeStyle;
         GUIStyle rootNodeStyle;
        [NonSerialized]
        DialogueNode draggingNode = null;
        [NonSerialized]
        DialogueNode creatingNode = null;
        [NonSerialized]
        DialogueNode deletingNode = null;
        [NonSerialized]
        DialogueNode linkingParentNode = null;
        [NonSerialized]
        Vector2 draggingOffset;
        Vector2 scrollPosition;
        [NonSerialized]
        bool draggingCanvas = false;
        [NonSerialized]
        bool zoomedout = false;
        [NonSerialized]
        Vector2 draggingCanvasOffset;
        [NonSerialized]
         string[] positionOptions = new string[] {"None","left", "middle", "right"};
        

        const float canvasBackgroundSize = 50;

        [MenuItem("Window/DialogueEditor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialouge Editor");
        }
        
        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceID , int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue ;
            if(dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }           
           return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.blue;
            nodeStyle.padding = new RectOffset(15, 20, 15, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            
            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerNodeStyle.normal.textColor = Color.blue;
            playerNodeStyle.padding = new RectOffset(15, 20, 15, 20);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);

            rootNodeStyle = new GUIStyle();
            rootNodeStyle.normal.background = EditorGUIUtility.Load("node5") as Texture2D;
            rootNodeStyle.normal.textColor = Color.yellow;
            rootNodeStyle.padding = new RectOffset(15, 20, 15, 20);
            rootNodeStyle.border = new RectOffset(12, 12, 12, 12);
            
         }

        
        private void OnSelectionChanged()
        {
           Dialogue newDialogue = Selection.activeObject as Dialogue;
           if (newDialogue != null)
           {
               selectedDialogue = newDialogue;
               Repaint();
           }
        }

        private void OnGUI()
        {
            if(selectedDialogue == null)
            {
                 EditorGUILayout.LabelField( "No Dialogue Seleceted.");
            }
            else
            {
                ProcessEvents();
                

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                Rect canvas = GUILayoutUtility.GetRect(selectedDialogue.canvasWidth, selectedDialogue.canvasHeight);  
                Texture2D backgroundTex = Resources.Load("CanBG") as Texture2D;
                Rect texCoords = new Rect(0, 0, selectedDialogue.canvasWidth / canvasBackgroundSize, selectedDialogue.canvasHeight / canvasBackgroundSize);
#if UNITY_EDITOR
                             
                if(backgroundTex !=null)
                {
                     GUI.DrawTextureWithTexCoords(canvas,backgroundTex, texCoords);
                } 
                  
#endif
               
                if(zoomedout)
                {
                    Matrix4x4 Translation = Matrix4x4.TRS(new Vector2(0,1),Quaternion.identity,Vector3.one);
                    Matrix4x4 Scale = Matrix4x4.Scale(new Vector3(0.8f, 0.8f, 0.8f));
                    
                    GUI.matrix = Translation*Scale*Translation.inverse;
                     Repaint();
                    
                } 
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                 foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);                   
                }
                 
                
                EditorGUILayout.EndScrollView();
                if(creatingNode!=null)
                {                
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null; 
                }
                if(deletingNode!=null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null; 
                }
                
                
            }       
        }

       

        public void ProcessEvents()
        {
            if(Event.current.type == EventType.MouseMove && Event.current.type != EventType.MouseDown)
            {
                draggingNode = null;
            }
            if(Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                if(zoomedout)
                {
                    //draggingNode= GetNodeAtPoint(Event.current.mousePosition + scrollPosition  + new Vector2(55f,115f));   
                    Repaint();
                }
                else
                {   
                    draggingNode= GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                }
                     

                if(draggingNode != null)
                {
                    draggingOffset = draggingNode.GetPos().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode; 
                }         
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if(Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetPos(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;  
            }
            else if(Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition =draggingCanvasOffset - Event.current.mousePosition;
                GUI.changed = true;
            }
            else if(Event.current.type == EventType.MouseUp && draggingNode != null)
            {               
                draggingNode = null;            
            }  
            else if(Event.current.type == EventType.MouseUp && draggingCanvas)
            {               
                draggingCanvas = false;            
            }     
            else if(Event.current.type == EventType.ScrollWheel )
            {            
                zoomedout=!zoomedout;
                //not working probably
                GUI.changed = true;
                Repaint();
            }        
        }

        
        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.IsPlayerSpeaking())
            {
                style = playerNodeStyle;
            }
            if(node.IsRootNode())
            {
                style =rootNodeStyle;
            }
            GUILayout.BeginArea(node.GetPos(), style);
            node.SetCharName(EditorGUILayout.TextField(node.GetCharName()));
            node.SetText(EditorGUILayout.TextField(node.GetText()));

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }
            DrawLinkButtons(node);

            if (GUILayout.Button("X"))
            {
                deletingNode = node;
            }

            GUILayout.EndHorizontal();

            int tempPositionIndex = EditorGUILayout.Popup(node.GetIndexPosition(),positionOptions);
            node.SetIndexPosition(tempPositionIndex);
            node.SetSpritePosition(positionOptions[tempPositionIndex]);

            CheckAssets(node);

            GUILayout.EndArea();
        }

        

        private void DrawLinkButtons(DialogueNode node)
        {
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    linkingParentNode = node;
                }
            }
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("cancel"))
                {                    
                    linkingParentNode = null;
                }
            }
            else if(linkingParentNode.GetChildren().Contains(node.name))
            {
                 if (GUILayout.Button("unlink"))
                {
                    linkingParentNode.RemoveChild(node.name);
                    node.SetParentName(null);
                    linkingParentNode = null;
                }
            }
            else 
            {
                if (GUILayout.Button("child"))
                {
                    if(linkingParentNode.GetParentName() == node.name)
                    {
                        Debug.Log("invalid connection");
                        linkingParentNode = null;
                    }
                    else
                    {
                        linkingParentNode.AddChild(node.name);
                        node.SetNodeNumber(linkingParentNode.GetNodeNumber()+1);
                        node.SetParentName(linkingParentNode.name);
                        linkingParentNode = null; 
                    }                   
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
             Vector3 startPosition = node.GetPos().center ;
                startPosition.x = node.GetPos().xMax;
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {              
                Vector3 endPosition = childNode.GetPos().center;
                endPosition.x = childNode.GetPos().xMin ;
                Vector3 controlPointOffset = new Vector2(100, 0);
                controlPointOffset.y=0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition ,endPosition ,
                    startPosition + controlPointOffset ,
                    endPosition - controlPointOffset ,
                     Color.cyan ,null , 4f);
                childNode.SetParentName(node.name);     
            }
        }


        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
           foreach(DialogueNode node in selectedDialogue.GetAllNodes())
           {
               if(node.GetPos().Contains(point))
               {
                  foundNode = node;            
               }
           }
           return foundNode;
        }

        private static void CheckAssets(DialogueNode node)
        {
            if (node.GetSpriteName() != null)
            {
                EditorGUILayout.LabelField($"Sprite : {node.GetSpriteName().name}");
            }
            else
            {
                EditorGUILayout.LabelField("Sprite : Empty");
            }

            if (node.GetBGName() != null)
            {
                EditorGUILayout.LabelField($"BG : {node.GetBGName().name}");
            }
            else
            {
                EditorGUILayout.LabelField("BG : Empty");
            }

            if (node.GetVLName() != null)
            {
                EditorGUILayout.LabelField($"Audio : {node.GetVLName().name}");
            }
            else
            {
                EditorGUILayout.LabelField("Audio : Empty");
            }
            if (node.IsSpriteVisible())
            {
                EditorGUILayout.LabelField("SpriteV : Switch");
            }
            else
            {
                EditorGUILayout.LabelField("SpriteV : No Switch");
            }
        }
    }
}

  
