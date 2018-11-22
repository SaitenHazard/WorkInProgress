using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTwo : DialogueBase
{
    public override void DoSpeech()
    {
        speech = new string[1];

        speech[0] = GameObject.Find("Quest1").GetComponent<Quest1>().GetEnemiesLeft().ToString();

        speech[0] += " enemies still left.";

        base.DoSpeech();
    }
}
