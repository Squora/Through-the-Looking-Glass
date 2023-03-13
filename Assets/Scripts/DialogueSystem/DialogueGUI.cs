using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueGUI : MonoBehaviour
{
    [SerializeField] private GameObject _messageBox;
    [SerializeField] private TextMeshProUGUI _npcText;
    [SerializeField] private TextMeshProUGUI _npcName;

    [SerializeField] private Button _buttonPrefab;
    [SerializeField] private GameObject _buttonsBox;

    public int offset = 20;
    private float curY, height;

    private List<GameObject> _buttons = new List<GameObject>();

    private DialogueReader _reader;

    private void Start()
    {
        _reader = FindObjectOfType<DialogueReader>();
        _messageBox.SetActive(false);
    }

    public void ShowDialogue(int current)
    {
        _messageBox.SetActive(true);
        _buttonsBox.SetActive(true);
        BuildDialogue(current);
    }

    public void HideDialogue()
    {
        _buttonsBox.SetActive(false);
        _messageBox.SetActive(false);
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
            clone.transform.position = new Vector2(950, 60f + i * offset);
            clone.GetComponentInChildren<TextMeshProUGUI>().text = _reader._node[currentNode].answer[i].text;


            if (_reader._node[currentNode].answer[i].toNode > 0)
            {
                SetNextDialogue(clone, _reader._node[currentNode].answer[i].toNode);
            }

            if (_reader._node[currentNode].answer[i].exit)
            {
                SetExitDialogue(clone);
            }

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
    }

    void SetExitDialogue(Button button) 
    {
        button.onClick.AddListener(() => HideDialogue());
    }
}
