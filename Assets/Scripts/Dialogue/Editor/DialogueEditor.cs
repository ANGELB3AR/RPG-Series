using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;
        [NonSerialized] GUIStyle nodeStyle;
        [NonSerialized] DialogueNode draggingNode = null;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] DialogueNode creatingNode = null;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            
            return false;
        }

        void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }

        void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected");
            }
            else
            {
                ProcessEvents();
                EditorGUILayout.LabelField(selectedDialogue.name);
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                    DrawNode(node);
                }
                if (creatingNode != null)
                {
                    Undo.RecordObject(selectedDialogue, "Add Dialogue Node");
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
            }
        }

        void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Move Dialogue Node");
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
        }

        void DrawNode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();

            string newText = EditorGUILayout.TextField(node.text);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialogue, "Update Dialogue Text");

                node.text = newText;
            }

            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }

            GUILayout.EndArea();
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.rect.xMax, node.rect.center.y);

            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.rect.xMin, childNode.rect.center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0f;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition, 
                    startPosition + controlPointOffset, 
                    endPosition - controlPointOffset, 
                    Color.white, null, 4f);
            }
        }

        DialogueNode GetNodeAtPoint(Vector2 mousePosition)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.rect.Contains(mousePosition))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}