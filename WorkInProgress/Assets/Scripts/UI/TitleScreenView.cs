using UnityEngine;
using UnityEngine.UI;
using System;

public class TitleScreenView : MonoBehaviour
{
    public static TitleScreenView Instance;

    public GameObject [] Slots;

    public RectTransform selectedTranform;
    public Image titleScreenBack; 

    private int indexVertical;
    private int indexHorizontal;
    private SaveLoadSystem saveLoadSystem;
    private RectTransform[][] slotChildTransforms = new RectTransform[3][];

    private void Awake()
    {
        Instance = this;

        titleScreenBack = GetComponent<Image>();
        saveLoadSystem = GetComponentInParent<SaveLoadSystem>();
    }

    private void Start()
    {
        indexVertical = 0;
        indexHorizontal = 1;

        for (int i = 0; i < Slots.Length; i++)
        {
            slotChildTransforms[i] = Slots[i].GetComponentsInChildren<RectTransform>(true);
        }

        UpdateSelected();
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

    public bool GetTitleScreenActive()
    {
        return titleScreenBack.IsActive();
    }

    public void ActionPresed()
    {
        string slotNumber = GetSlotName();

        if (indexHorizontal == 1)
            saveLoadSystem.DoNewGame(slotNumber);
        else
            saveLoadSystem.LoadGame(slotNumber);
    }

    private string GetSlotName()
    {
        int slotNumber = indexVertical + 1;
        string slotName = "Slot" + slotNumber;
        return slotName;
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

        if (right)
        {
            indexHorizontal--;

            if (indexHorizontal == 0)
                indexHorizontal = 2;
        }

        else if (left)
        {
            indexHorizontal++;

            if (indexHorizontal == 3)
                indexHorizontal = 1;
        }

        UpdateSelected();
    }

    private void UpdateSelected()
    {
        if (slotChildTransforms[indexVertical][indexHorizontal].
            GetComponent<Text>().IsActive() == false)
            indexHorizontal--;

        selectedTranform.position = slotChildTransforms[indexVertical][indexHorizontal].position;
    }
}



