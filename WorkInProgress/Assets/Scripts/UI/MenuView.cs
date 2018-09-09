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
    private int mainMenuIndex;
    private bool active = false;

    private void Awake()
    {
        Instance = this;

        mainMenuTexts = mainMenuObject.GetComponentsInChildren<Text>();
    }

    public bool GetMenuActive()
    {
        return active;
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

    public void SetMenuActive(bool state)
    {
        active = state;
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

    private void UpdateView()
    {
        SetMenuEnable();

        if (active == false)
            return;

        slected.transform.position = mainMenuObject.transform.position;
        subMenuObjects[mainMenuIndex].SetActive(true);

        for(int i = 0; i < 4; i++)
        {
            if(mainMenuIndex != i)
                subMenuObjects[i].SetActive(false);
        }
    }   
}
