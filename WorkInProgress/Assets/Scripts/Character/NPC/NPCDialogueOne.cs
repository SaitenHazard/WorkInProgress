using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueOne : DialogueBase
{
    private GameObject questGameObject;
    private QuestBase questBase;

    private void Awake()
    {
        questGameObject = GameObject.Find("Quest1");
        questBase = questGameObject.GetComponent<QuestBase>();
    }

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
                            questBase.Activate();
                            return;
                        }
                    }
                }
            }
        }

        base.DoSpeech();
    }

    private void SetDiloagueWhenActive()
    {

    }
}
