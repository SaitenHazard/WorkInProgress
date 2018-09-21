using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechTextUI : MonoBehaviour
{
    public static SpeechTextUI Instance;

    private Text[] texts;
    private Image[] images;

    private PlayTimeUI playTimeUI;

    private void Awake()
    {
        Instance = this;

        texts = GetComponentsInChildren<Text>();
        images = GetComponentsInChildren<Image>();
        playTimeUI = PlayTimeUI.instance;
    }

    private void Start()
    {
        ActivateSpeechBox(false);
    }

    public void ActivateSpeechBox(bool active)
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].enabled = active;

        for (int i = 0; i < images.Length; i++)
            images[i].enabled = active;

        //playTimeUI.Activate(!active);
    }

    public bool GetTextBoxActive()
    {
        return images[0].IsActive();
    }

    public void SetString(string speach, string speaker)
    {
        texts[0].text = speach;
        texts[1].text = speaker;
    }
}
