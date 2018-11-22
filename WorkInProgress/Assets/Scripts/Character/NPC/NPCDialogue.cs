using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : DialogueBase
{
    public override void DoSpeech()
    {
        if(index != -1)
        {
            if (index < speech.Length)
            {
                if (optionsIndexes[index] == true)
                {
                    if (index == 2)
                    {
                        int optionIndex = DialogueTextUI.Instance.GetOptionIndex();

                        if (optionIndex == 0)
                        {
                            Debug.Log("Does it work?...");
                        }
                    }
                }
            }
        }

        base.DoSpeech();
    }
}
