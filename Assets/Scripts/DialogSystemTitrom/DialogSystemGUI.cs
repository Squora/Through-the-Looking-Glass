using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class DialogSystemGUI : MonoBehaviour
{
    [Header("MessageDialog")]
    [SerializeField] private GameObject messageBox;

    [SerializeField] private TextMeshProUGUI nameMessage—ompanion;

    [SerializeField] private TextMeshProUGUI textMessageDialog;

    [Header("QusetionDialog")]
    [SerializeField] private GameObject qusetionBox;

    [SerializeField] private TextMeshProUGUI nameQusetion—ompanion;

    [SerializeField] private TextMeshProUGUI textQusetionDialog;

    [SerializeField] private GameObject answers;

    [SerializeField] private GameObject answersPrefab;

    public void DisplayGUIDialog(Dialog nextDialog)
    {
        foreach (Transform child in answers.transform) Destroy(child.gameObject);

        if (nextDialog.DialogType.Equals(TypeDialog.Message))
        {
            qusetionBox.SetActive(false);

            messageBox.SetActive(true);

            nameMessage—ompanion.text = nextDialog.Name—ompanion;
            textMessageDialog.text = nextDialog.Message;
        }
        else if (nextDialog.DialogType.Equals(TypeDialog.Qusetion))
        {
            messageBox.SetActive(false);

            qusetionBox.SetActive(true);

            textQusetionDialog.text = nextDialog.Qusetion;
            foreach (var answer in nextDialog.Answers)
            {
                var answerUI = Instantiate(answersPrefab, answers.transform);
                answerUI.GetComponent<TextMeshProUGUI>().text = answer.Text;

                answerUI.GetComponent<Button>().onClick.AddListener(() => { NextDialogToAnswer(answer.NextDialogs); });
            }
        }

    }

    private void NextDialogToAnswer(List<Dialog> dialogs)
    {
        FindObjectOfType<DialogSystem>().StartDialog(dialogs);
    }

    public void EndDialog()
    {
        messageBox.SetActive(false);
        qusetionBox.SetActive(false);

    }
}

