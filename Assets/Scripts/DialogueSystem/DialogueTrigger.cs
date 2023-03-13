using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string _fileName;
    [SerializeField] private DialogueReader _reader;

    //private InputAction.CallbackContext context;

    //bool trigger = false;

    private void Start()
    {
        _reader = FindObjectOfType<DialogueReader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _reader.StartDialogue(_fileName);
        Debug.Log($"Dialogue with {_reader._node[0].npcName} is started");
        //trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _reader.StopDialogue();
        Debug.Log($"Dialogue with {_reader._node[0].npcName} is ended");
    }

    //public void T()
    //{
    //    reader.DialogueStart(fileName);
    //}
}
