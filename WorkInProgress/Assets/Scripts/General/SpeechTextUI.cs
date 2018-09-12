using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechTextUI : MonoBehaviour
{
    public static SpeechTextUI Instance;

    private Text text;
    private Image back;

    public GameObject playTimeUI;

    private void Awake()
    {
        Instance = this;

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
