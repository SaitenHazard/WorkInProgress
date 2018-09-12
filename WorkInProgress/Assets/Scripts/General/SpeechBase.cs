using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBase : MonoBehaviour
{
    public string[] speech;
    public Vector2[] faceDirection;
    public GameObject[] Object;
    public enumSpeechBubbles[] speechBubble;

    private int index;
    private SpeechTextUI speechTextUI;

    private void Awake()
    {
        speechTextUI = SpeechTextUI.Instance.GetComponent<SpeechTextUI>();
    }

    public void Initialize()
    {
        index = -1;
    }

    public void DoSpeech()
    {
        index++;

        speechTextUI.SetString(speech[index]);
    }
}