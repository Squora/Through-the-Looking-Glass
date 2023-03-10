using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.TerrainTools;
using UnityEngine.UI;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum TypeDialog
{
    Message,
    Qusetion
}
[Serializable]
public class Dialog: MonoBehaviour
{
    public string NameŅompanion;

    public TypeDialog DialogType;

    public string Message;

    public string Qusetion;

    public List<Answer> Answers;

    [Serializable]
    public class Answer
    {
        public string Text;
        public List<Dialog> NextDialogs;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Dialog))]
[CanEditMultipleObjects]
public class DialogEditor: Editor
{
    Dialog dialog;

    SerializedProperty dialogType;
    SerializedProperty nameŅompanion;

    SerializedProperty qusetion;


    SerializedProperty message;

    SerializedProperty answers;


    private void OnEnable()
    {
        dialog = (Dialog)target;

        nameŅompanion = serializedObject.FindProperty("NameŅompanion");

        dialogType = serializedObject.FindProperty("DialogType");
        message = serializedObject.FindProperty("Message");

        qusetion = serializedObject.FindProperty("Qusetion");
        answers = serializedObject.FindProperty("Answers");

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(nameŅompanion);

        EditorGUILayout.PropertyField(dialogType);

        if (dialog.DialogType.Equals(TypeDialog.Message))
        {
            EditorGUILayout.PropertyField(message);
            
        }
        else if (dialog.DialogType.Equals(TypeDialog.Qusetion))
        {
            EditorGUILayout.PropertyField(qusetion);

            EditorGUILayout.PropertyField(answers);
        }

        serializedObject.ApplyModifiedProperties();

    }

}
#endif
