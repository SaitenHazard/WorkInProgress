using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpeech : InteractableBase
{
    public override void OnInteract()
    {
        SaveLoadSystem.Instance.SaveGame();
    }
}
