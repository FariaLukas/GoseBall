using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text messageText;
    public Text charName;
    public Image charImage;
    public GameObject dialoguePopUp;
    public List<DialogueSetup> setups;
    private int index;

    public void ShowDialogue()
    {
        UpdateDialogue();
        dialoguePopUp.SetActive(true);
    }

    private void UpdateDialogue()
    {
        messageText.text = setups[index].message;
        charName.text = setups[index].charName;
        charImage.sprite = setups[index].charSprite;
    }

    public void NextDialogue()
    {
        index++;

        if (index >= setups.Count)
        {
            HideDialogue();
        }
        else
        {
            UpdateDialogue();
        }


    }

    private void HideDialogue()
    {
        dialoguePopUp.SetActive(false);
    }

}

[System.Serializable]
public class DialogueSetup
{
    public string charName;
    [TextArea]
    public string message;
    public Sprite charSprite;
}
