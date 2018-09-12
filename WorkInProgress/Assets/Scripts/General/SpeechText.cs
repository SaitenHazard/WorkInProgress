using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechText : MonoBehaviour
{
    private Text text;
    private Image back;

    public GameObject playTimeUI;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        back = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        ActivateSpeechBox(false);
    }

    public void ActivateSpeechBox(bool active)
    {
        text.enabled = active;
        back.enabled = active;

        PlayTimeUI.instance.Activate(!active);
    }

    public bool GetTextBoxActive()
    {
        return text.IsActive();
    }

    public void SetString(string String)
    {
        text.text = String;
    }
}
