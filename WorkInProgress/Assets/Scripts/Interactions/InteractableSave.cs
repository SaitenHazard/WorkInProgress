using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSave : InteractableBase
{
    public override void OnInteract()
    {
        Debug.Log("Save");
        return;

        SaveLoadSystem.Instance.SaveGame();
    }
}
