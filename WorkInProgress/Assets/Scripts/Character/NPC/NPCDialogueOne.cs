using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueOne : DialogueBase
{
    private GameObject questGameObject;
    private QuestBase questBase;

    private void Awake()
    {
        GameObject questGameObject = GameObject.Find("Quest1");
        QuestBase questBase = questGameObject.GetComponent<QuestBase>();
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
                            startingIndex = 2;
                            finishingIndex = 4;
                        }
                    }
                }
            }
        }

        base.DoSpeech();

        if(questGameObject != null)
        speech[3] = GameObject.Find("Quest1").GetComponent<Quest1>().GetEnemiesLeft().ToString();
        speech[3] += " enemies still left.";
    }

    private void SetDiloagueWhenActive()
    {

    }
}
