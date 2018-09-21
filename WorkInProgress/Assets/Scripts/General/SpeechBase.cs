using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBase : MonoBehaviour
{
    public string[] speech;
    public string[] speaker;
    public Vector2[] faceDirection;
    public GameObject[] Object;
    public enumSpeechBubbles[] speechBubbleEnum;

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

        speechTextUI.SetString(speech[index], speaker[index]);

        if(Object[index] == null)
        {
            return;
        }

        if (Object[index].name == "PlayerStandIn")
            Object[index] = PlayerInstant.Instance.gameObject;


        if (speechBubbleEnum[index] != enumSpeechBubbles.NULL)
        {
            SpeechBubble n_speechBubble = Object[index].GetComponentInChildren<SpeechBubble>();
            n_speechBubble.PopSpeechBubble(speechBubbleEnum[index]);
        }

        if (faceDirection[index] != new Vector2(0, 0))
        {
            CharacterMovementModel n_CharacterMovementModel = Object[index].GetComponent<CharacterMovementModel>();
            n_CharacterMovementModel.SetDirection(faceDirection[index]);
        }
    }
}