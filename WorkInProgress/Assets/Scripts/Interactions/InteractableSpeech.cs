using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpeech : InteractableBase
{
    public override void OnInteract()
    {
        SpeechBase speechBase = GetComponent<SpeechBase>();
        speechBase.Initialize();
        speechBase.DoSpeech();
    }
}
