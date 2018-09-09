using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public static MenuView Instance;
    public RectTransform[] buttonTransform;
    public GameObject[] menuObjects;
    public Transform slected;

    private int mainMenuIndex;
    private bool active = true;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        mainMenuIndex = 0;
    }

    private void Update()
    {
        UpdateView();
    }

    public void ChangeIndex(bool increment)
    {
        if (increment)
        {
            mainMenuIndex--;

            if (mainMenuIndex == -1)
                mainMenuIndex = 4;
        }
        else
        {
            mainMenuIndex++;

            if (mainMenuIndex == 5)
                mainMenuIndex = 0;
        }
    }

    private void UpdateView()
    {
        gameObject.SetActive(active);

        if (active == false)
            return;

        slected.position = buttonTransform[mainMenuIndex].position;
        menuObjects[mainMenuIndex].SetActive(true);

        for(int i = 0; i < 4; i++)
        {
            if(mainMenuIndex != i)
                menuObjects[i].SetActive(false);
        }
    }
}
