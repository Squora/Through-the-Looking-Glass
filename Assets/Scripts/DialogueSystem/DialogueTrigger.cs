using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string _fileName;
    [SerializeField] private DialogueReader _reader;

    public bool EnableDebugging = true;
    //private InputAction.CallbackContext context;

    //bool trigger = false;

    private void Start()
    {
        _reader = FindObjectOfType<DialogueReader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _reader.StartDialogue(_fileName);
        if (EnableDebugging) Debug.Log($"Dialogue with {_reader._node[0].npcName} is started");
        //trigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _reader.StopDialogue();
        if (EnableDebugging) Debug.Log($"Dialogue with {_reader._node[0].npcName} is ended");
    }

    //public void T()
    //{
    //    reader.DialogueStart(fileName);
    //}
}
