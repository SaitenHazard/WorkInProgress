using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechTextUI : MonoBehaviour
{
    public static SpeechTextUI Instance;

    private Text text;
    private Image back;

    private PlayTimeUI playTimeUI;

    private void Awake()
    {
        Instance = this;

        text = GetComponentInChildren<Text>();
        back = GetComponentInChildren<Image>();
        playTimeUI = PlayTimeUI.instance;
    }

    private void Start()
    {
        ActivateSpeechBox(false);
    }

    public void ActivateSpeechBox(bool active)
    {
        text.enabled = active;
        back.enabled = active;

        playTimeUI.Activate(!active);
    }

    public bool GetTextBoxActive()
    {
        return back.IsActive();
    }

    public void SetString(string String)
    {
        text.text = String;
    }
}
