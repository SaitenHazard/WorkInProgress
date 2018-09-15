using UnityEngine;
using System;

public class TitleScreenView : MonoBehaviour
{
    public static TitleScreenView Instance;

    public GameObject[] Slots;
    public Transform selectedTranform;

    private int indexVertical;
    private int indexHorizontal;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        indexVertical = 0;
        indexHorizontal = 0;
    }

    private void Update()
    {
        UpdateControl();
    }

    private void UpdateControl()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ChangeIndex(true, false, false, false);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            ChangeIndex(false, true, false, false);

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            ChangeIndex(false, false, true, false);

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            ChangeIndex(false, false, false, true);
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
        Transform []childTransforms = Slots[indexVertical].GetComponentsInChildren<Transform>();

        selectedTranform.position = childTransforms[indexHorizontal].position;
    }
}



