using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogue : InteractableBase
{
    private DialogueBase speechBase;

    private void Awake()
    {
        base.Awake();
        speechBase = GetComponent<DialogueBase>();
    }

    public override void OnInteract()
    {
        speechBase.Initialize();
        speechBase.DoSpeech();
    }
}
