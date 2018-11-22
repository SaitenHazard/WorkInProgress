using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueOne : DialogueBase
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
                            GameObject questGameObject = GameObject.Find("Quest1");
                            questGameObject.GetComponent<QuestBase>().Activate();
                            Destroy(this);
                        }
                    }
                }
            }
        }

        base.DoSpeech();
    }
}
