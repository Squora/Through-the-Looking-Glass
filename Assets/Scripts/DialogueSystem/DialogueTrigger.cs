using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private DialogueReader reader;

    private InputAction.CallbackContext context;

    //bool trigger = false;

    private void Start()
    {
        reader = FindObjectOfType<DialogueReader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        reader.DialogueStart(fileName);
        //trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        reader.DialogueStop();
    }

    //public void T()
    //{
    //    reader.DialogueStart(fileName);
    //}
}
