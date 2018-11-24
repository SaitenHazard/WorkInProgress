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

    override public void Initialize()
    {
        if (questBase.IsActive() == false)
        {
            startingIndex = 0;
            finishingIndex = 3;
        }

        if (questBase.IsActive() == true)
        {
            startingIndex = 3;
            finishingIndex = 4;
            speech[3] = questBase.GetEnemiesLeft().ToString() + " enemie(s) still left";
        }

        if (questBase.IsComplete() == true)
        {
            startingIndex = 4;
            finishingIndex = 5;
            speech[4] = "THE WHOLE SHABANG WORKS!";
        }

        index = startingIndex - 1;

        Debug.Log("index = " + index);
        Debug.Log("start = " + startingIndex);
        Debug.Log("finish = " + finishingIndex);
    }

    public override void DoSpeech()
    {
        if (index != -1)
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
                        }
                    }
                }
            }
        }

        base.DoSpeech();
    }
}
