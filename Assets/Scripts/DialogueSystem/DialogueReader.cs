using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class DialogueReader : MonoBehaviour
{
    public string folder = "Russian";

    private string _fileName, _lastName;
    public List<Dialogue> _node;
    private Dialogue _dialogue;
    private Answer _answer;
    private DialogueGUI _gui;

    public bool EnableDebugging = true;

    void Start()
    {
        _gui = FindObjectOfType<DialogueGUI>();
    }

    public void StartDialogue(string name)
    {
        if (name == string.Empty) return;
        _fileName = name;
        ParseDialogueFile();
    }

    public void StopDialogue() 
    { 
        _gui.HideDialogue();
    }

    void ParseDialogueFile()
    {
        if (_lastName == _fileName)
        {
            _gui.ShowDialogue(0);
            return;
        }

        _node = new List<Dialogue>();

        try
        {
            TextAsset binary = Resources.Load<TextAsset>(folder + "/" + _fileName);
            XmlTextReader reader = new XmlTextReader(new StringReader(binary.text));

            int index = 0;
            while (reader.Read())
            {
                if (reader.IsStartElement("node"))
                {
                    _dialogue = new Dialogue();
                    _dialogue.answer = new List<Answer>();
                    _dialogue.npcName = reader.GetAttribute("npcName");
                    _dialogue.npcText = reader.GetAttribute("npcText");
                    _node.Add(_dialogue);

                    XmlReader inner = reader.ReadSubtree();
                    while (inner.ReadToFollowing("answer"))
                    {
                        _answer = new Answer();
                        _answer.text = reader.GetAttribute("text");

                        int number;
                        if (int.TryParse(reader.GetAttribute("toNode"), out number)) _answer.toNode = number; else _answer.toNode = 0;

                        bool result;
                        if (bool.TryParse(reader.GetAttribute("exit"), out result)) _answer.exit = result; else _answer.exit = false;

                        _node[index].answer.Add(_answer);
                    }
                    inner.Close();

                    index++;
                }
            }

            _lastName = _fileName;
            reader.Close();
            if (EnableDebugging) Debug.Log("XML file was successfully read");
        }
        catch (System.Exception error)
        {
            if (EnableDebugging) Debug.Log(this + " Ошибка чтения файла диалога: " + _fileName + ".xml >> Error: " + error.Message);
            _lastName = string.Empty;
        }
        _gui.ShowDialogue(0);
    }
}

