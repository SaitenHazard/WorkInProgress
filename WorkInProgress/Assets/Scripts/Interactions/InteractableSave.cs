using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSave : InteractableBase
{
    SpeechBase speechBase;

    private void Awake()
    {
        speechBase = GetComponent<SpeechBase>();
        base.Awake();
    }

    public override void OnInteract()
    {
        speechBase.Initialize();
        speechBase.DoSpeech();
        SaveLoadSystem.Instance.DoSaveGame();
        return;
    }
}
