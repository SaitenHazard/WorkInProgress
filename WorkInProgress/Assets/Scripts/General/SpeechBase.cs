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

        if (index == 0)
            SpeechTextUI.Instance.ActivateSpeechBox(true);

        if (index == speech.Length)
        {
            SpeechTextUI.Instance.ActivateSpeechBox(false);
            return;
        }

        speechTextUI.SetString(speech[index]);

        if(Object[index] == null)
        {
            return;
        }

        if (Object[index].name == "PlayerStandIn")
            Object[index] = PlayerInstant.Instance.gameObject;

        Debug.Log(Object[index])

        if (speech[index] != null)
            Object[index].GetComponent<SpeechBubble>().PopSpeechBubble(speechBubble[index]);

        if (faceDirection[index] != new Vector2(0, 0))
            Object[index].GetComponent<CharacterMovementModel>().SetDirection(faceDirection[index]);
    }
}