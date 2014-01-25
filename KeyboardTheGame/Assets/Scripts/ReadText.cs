using UnityEngine;
using System.Collections;

public class ReadText : MonoBehaviour {

    public string stringToMatch = "toto123";

    bool hasBeenTriggered = false;
    string enteredText;

    public void Start()
    {
        ResetEnteredText();
    }

    public void OnEnterTile()
    {
        hasBeenTriggered = true;
    }

    public void OnExitTile()
    {
        ResetEnteredText();
        hasBeenTriggered = false;
    }

    public void OnGUI()
    {
        if (hasBeenTriggered)
        {
            enteredText = GUILayout.TextField(enteredText);
            if (string.Compare(enteredText, stringToMatch) == 0)
            {
                ResetEnteredText();
                hasBeenTriggered = false;
            }
        }
    }

    void ResetEnteredText()
    {
        enteredText = "Enter Password here";
    }
}
