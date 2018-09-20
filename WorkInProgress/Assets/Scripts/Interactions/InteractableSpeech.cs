using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpeech : InteractableBase
{
    private SpeechBase speechBase;

    private void Awake()
    {
        base.Awake();
        speechBase = GetComponent<SpeechBase>();
    }

    public override void OnInteract()
    {
        speechBase.Initialize();
        speechBase.DoSpeech();
    }
}
