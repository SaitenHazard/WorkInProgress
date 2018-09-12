using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimeUI : MonoBehaviour {

    public static PlayTimeUI instance;

    private Image[] childImages;
    private Text[] childTexts;

    private void Awake()
    {
        childImages = GetComponentsInChildren<Image>();
        childTexts = GetComponentsInChildren<Text>();
    }

    public void Activate(bool active)
    {
        for (int i = 0; i < childImages.Length; i++)
            childImages[i].enabled = active;

        for (int i = 0; i < childTexts.Length; i++)
            childTexts[i].enabled = active;
    }
}
