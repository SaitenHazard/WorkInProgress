using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechText : MonoBehaviour
{
    private Text text;
    private Image back;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        back = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        ActivateSpeechBox(false);
    }

    private void ActivateSpeechBox(bool active)
    {
        text.enabled = active;
        back.enabled = active;
    }

    public bool GetTextBoxActive()
    {
        return text.IsActive();
    }

    public void DoTextBox(string [] String)
    {

    }
}
