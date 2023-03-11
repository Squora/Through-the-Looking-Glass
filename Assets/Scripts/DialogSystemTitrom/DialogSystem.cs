using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogSystem : MonoBehaviour
{

    private Queue<Dialog> sentences;

    private DialogSystemGUI systemGUI;

    public event Action<TypeDialog> onNextDialog;

    public event Action onMove;

    private void Awake()
    {
        systemGUI = GetComponent<DialogSystemGUI>();
    }

    private void Start()
    {
        sentences= new Queue<Dialog>();
    }

    public void StartDialog(List<Dialog> dialogs)
    {
        sentences.Clear();

        foreach(var dialog in dialogs) 
        {
            
            sentences.Enqueue(dialog);
        }
        DisplayNextSentense();
    }


    public void DisplayNextSentense()
    {
        if (sentences.Count == 0) 
        {
            EndDialog();
            return;
        }

        var sentence = sentences.Dequeue();
        onNextDialog.Invoke(sentence.DialogType);
        systemGUI.DisplayGUIDialog(sentence);
    }

    private void EndDialog()
    {
        systemGUI.EndDialog();

        onMove.Invoke();
    }
}
