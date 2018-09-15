using UnityEngine;
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

    void Awake()
    {
        Instance = this;

        titleScreenBack = GetComponent<Image>();
    }

    public bool GetTitleScreenActive()
    {
        return titleScreenBack.IsActive();
    }

    private void Start()
    {
        indexVertical = 0;
        indexHorizontal = 0;
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

    public int GetSlotIndex()
    {
        return indexVertical;
    }

    public int Get

    private void UpdateSelected()
    {
        Transform []childTransforms = Slots[indexVertical].
            GetComponentsInChildren<Transform>();

        selectedTranform.position = childTransforms[indexHorizontal+1].position;
    }
}



