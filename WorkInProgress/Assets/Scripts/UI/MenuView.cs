using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public static MenuView Instance;

    public GameObject mainMenuObject;
    public GameObject[] subMenuObjects;
    public GameObject slected;

    private Text[] mainMenuTexts;
    private int mainMenuIndex = 1;
    private bool active;

    private void Awake()
    {
        Instance = this;
        mainMenuTexts = mainMenuObject.GetComponentsInChildren<Text>();
    }

    private void Start()
    {
        SetMenuActive(false);
    }

    public bool GetMenuActive()
    {
        return active;
    }

    public void ChangeIndex(bool increment)
    {
        if (increment)
        {
            mainMenuIndex--;

            if (mainMenuIndex == 0)
                mainMenuIndex = 4;
        }
        else
        {
            mainMenuIndex++;

            if (mainMenuIndex == 5)
                mainMenuIndex = 1;
        }

        UpdateSelected();
    }

    public void SetMenuActive(bool state)
    {
        active = state;
        SetMenuEnable();
    }

    private void SetMenuEnable()
    {
        GetComponent<Image>().enabled = active;
        slected.GetComponent<Image>().enabled = active;

        for (int i = 0; i < 4; i++)
        {
            subMenuObjects[i].GetComponentInChildren<Text>().enabled = active;
        }

        for (int i = 0; i < mainMenuTexts.Length; i++)
        {
            mainMenuTexts[i].enabled = active;
        }
    }

    private void UpdateSelected()
    {
        slected.transform.position = mainMenuTexts[mainMenuIndex].transform.position;

        for (int i = 0; i < 4; i++)
        {
            subMenuObjects[i].SetActive(false);
        }

        subMenuObjects[mainMenuIndex-1].SetActive(true);
    }
}
