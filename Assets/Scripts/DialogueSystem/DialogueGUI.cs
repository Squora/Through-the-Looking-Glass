using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueGUI : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TextMeshProUGUI _npcText;
    [SerializeField] private TextMeshProUGUI _npcName;
    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private GameObject _buttonsBox;

    [Header("Visual params")]
    //[SerializeField] private float _dialogueBoxHeigthFactor = 1f;
    //[SerializeField] private float _dialogueBoxWidthFactor = 1f;
    //[SerializeField] private float _buttonWidthFactor = 1f;
    //[SerializeField] private float _buttonHeigthFactor = 1f;
    [SerializeField] private float _buttonWidthPosFactor = 1.15f;
    [SerializeField] private float _buttonHeigthPosFactor = 50f;
    [SerializeField] private int _offset = 60;

    [Header("Debugging params")]
    public bool EnableDebugging = true;

    private List<GameObject> _buttons = new List<GameObject>();

    private DialogueReader _reader;


    private void Start()
    {
        _reader = FindObjectOfType<DialogueReader>();
        _dialogueBox.SetActive(false);
    }

    public void ShowDialogue(int current)
    {
        _dialogueBox.SetActive(true);
        _buttonsBox.SetActive(true);
        //_dialogueBox.transform.localScale = new Vector2(_dialogueBoxWidthFactor, _dialogueBoxHeigthFactor);
        _dialogueBox.transform.localScale = Vector2.one;
        BuildDialogue(current);
    }

    public void HideDialogue()
    {
        _buttonsBox.SetActive(false);
        _dialogueBox.SetActive(false);
        //ClearDialogue();
    }

    void BuildDialogue(int currentNode)
    {
        ClearDialogue();
        _npcName.text = _reader._node[currentNode].npcName;
        _npcText.text = _reader._node[currentNode].npcText;

        for (int i = 0; i < _reader._node[currentNode].answer.Count; i++)
        {
            Button clone = Instantiate(_buttonPrefab);
            clone.transform.SetParent(_buttonsBox.transform, false);
            clone.gameObject.SetActive(true);
            //clone.transform.localScale = new Vector2(_buttonWidthFactor, _buttonHeigthFactor);
            clone.transform.localScale = Vector2.one;
            clone.transform.position = new Vector2(Screen.width / _buttonWidthPosFactor, _buttonHeigthPosFactor + i * _offset);
            clone.GetComponentInChildren<TextMeshProUGUI>().text = _reader._node[currentNode].answer[i].text;

            //clone.GetComponent<RectTransform>().sizeDelta = new Vector2(clone.GetComponent<RectTransform>().sizeDelta.x,
                //clone.GetComponentInChildren<TextMeshProUGUI>().text.Length + _offset);

            if (_reader._node[currentNode].answer[i].toNode > 0) SetNextDialogue(clone, _reader._node[currentNode].answer[i].toNode);
            if (_reader._node[currentNode].answer[i].exit) SetExitDialogue(clone);

            _buttons.Add(clone.gameObject);
        }
    }

    void ClearDialogue()
    {
        _npcText.text = string.Empty;
        _npcName.text = string.Empty;

        foreach (var button in _buttons)
        {
            Destroy(button.gameObject);
        }
    }

    void SetNextDialogue(Button button, int id)
    {
        button.onClick.AddListener(() => BuildDialogue(id));
        if (EnableDebugging) button.onClick.AddListener(() => Debug.Log($"Был выбран ответ toNode: {id}"));
    }

    void SetExitDialogue(Button button) 
    {
        button.onClick.AddListener(() => HideDialogue());
        if (EnableDebugging) button.onClick.AddListener(() => Debug.Log($"Ответ привел к завершению диалога"));
    }
}
