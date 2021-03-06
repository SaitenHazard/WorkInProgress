﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextUI : MonoBehaviour
{
    public static DialogueTextUI Instance;
    public Transform optionSelect;

    private Text[] dTexts;
    private Image[] dImages;

    private Text[] dOTexts;
    private Image[] dOImages;

    private PlayTimeUI playTimeUI;

    private int optionIndex;

    private void Awake()
    {
        Instance = this;
        playTimeUI = PlayTimeUI.instance;

        Transform transformDialogue = transform.Find("Dialogue");
        Transform transformDialogueOptions = transform.Find("DialogueOptions");

        dTexts = transformDialogue.GetComponentsInChildren<Text>();
        dImages = transformDialogue.GetComponentsInChildren<Image>();

        dOTexts = transformDialogueOptions.GetComponentsInChildren<Text>();
        dOImages = transformDialogueOptions.GetComponentsInChildren<Image>();
    }

    public int GetOptionIndex()
    {
        return optionIndex;
    }

    public void SetOptionIndex(int index)
    {
        optionIndex = index;
    }

    private void Start()
    {
        ActivateDialogueBox(false);
        ActivateDialogueOptionsBox(false);
    }

    private void Update()
    {
        optionSelect.position = new Vector2(dOTexts[optionIndex].transform.position.x - 0.45f, dOTexts[optionIndex].transform.position.y + 0.02f);
    }

    public void IncrementOptionIndex()
    {
        if (optionIndex == 1)
            optionIndex = 0;
        else
            optionIndex = 1;
    }

    public void ActivateDialogueOptionsBox(bool active, string option1, string option2)
    {
        optionIndex = 1;

        for (int i = 0; i < dOTexts.Length; i++)
            dOTexts[i].enabled = active;
        

        for (int i = 0; i < dOImages.Length; i++)
            dOImages[i].enabled = active;

        dOTexts[0].text = option1;
        dOTexts[1].text = option2;
    }

    public void ActivateDialogueOptionsBox(bool active)
    {
        ActivateDialogueOptionsBox(active, null, null);
    }

    public void ActivateDialogueBox(bool active)
    {
        for (int i = 0; i < dTexts.Length; i++)
            dTexts[i].enabled = active;

        for (int i = 0; i < dImages.Length; i++)
            dImages[i].enabled = active;

        playTimeUI.Activate(!active);
    }

    public bool GetDialogueBoxActive()
    {
        return dImages[0].IsActive();
    }

    public bool GetDialogueOptionBoxActive()
    {
        return dOImages[0].IsActive();
    }

    public void SetString(string speech)
    {
        dTexts[0].text = speech;
    }
}
