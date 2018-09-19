﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class TitleScreenView : MonoBehaviour
{
    public static TitleScreenView Instance;

    public GameObject[] Slots;
    public Transform selectedTranform;
    public Image titleScreenBack; 

    private int indexVertical;
    private int indexHorizontal;

    private void Awake()
    {
        Instance = this;

        titleScreenBack = GetComponent<Image>();
    }

    public void SetActive(bool active)
    {
        Image [] image = GetComponentsInChildren<Image>();
        Text [] text = GetComponentsInChildren<Text>();

        for (int i = 0; i < image.Length; i++)
            image[i].enabled = active;

        for (int i = 0; i < text.Length; i++)
            text[i].enabled = active;
    }

    private void Start()
    {
        indexVertical = 0;
        indexHorizontal = 0;
        UpdateSelected();
    }

    public bool GetTitleScreenActive()
    {
        return titleScreenBack.IsActive();
    }

    public void ActionPresed()
    {
        if (indexHorizontal == 0)
            DoNewSave();
        else
            ;
    }

    private void DoNewSave()
    {
        SaveLoadSystem saveLoadSystem = GetComponentInParent<SaveLoadSystem>();
        int slotNumber = indexVertical + 1;

        string slotName = "Slot" + slotNumber;

        saveLoadSystem.DoNewGame(slotName);
    }

    public void ChangeIndex(bool up, bool down, bool right, bool left)
    {
        if (up)
        {
            indexVertical--;

            if (indexVertical == -1)
                indexVertical = 2;
        }

        else if(down)
        {
            indexVertical++;

            if (indexVertical == 3)
                indexVertical = 0;
        }

        else if (right)
        {
            indexHorizontal--;

            if (indexHorizontal == -1)
                indexHorizontal = 1;
        }

        else if (left)
        {
            indexHorizontal++;

            if (indexHorizontal == 2)
                indexHorizontal = 0;
        } 

        UpdateSelected();
    }

    private void UpdateSelected()
    {
        Transform []childTransforms = Slots[indexVertical].
            GetComponentsInChildren<Transform>();

        selectedTranform.position = childTransforms[indexHorizontal+1].position;
    }
}



