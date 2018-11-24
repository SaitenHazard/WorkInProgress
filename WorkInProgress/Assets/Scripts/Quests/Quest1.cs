using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : QuestBase
{
    private int maxNumberOfEnemies;
    private int numberOfEnemiesLeft;

    public override void Activate()
    {
        AIBase[] aiScripts = GetComponentsInChildren<AIBase>(true);

        Debug.Log(aiScripts.Length);

        for (int i = 0; i < aiScripts.Length; i++)
        {
            aiScripts[i].transform.parent.gameObject.SetActive(true);
        }

        maxNumberOfEnemies = aiScripts.Length;

        active = true;
    }

    private void Update()
    {
        if(complete == false)
        {
            AIBase[] aiScripts = GetComponentsInChildren<AIBase>();
            numberOfEnemiesLeft = aiScripts.Length;

            if (IsActive() && GetEnemiesLeft() == 0)
                complete = true;
        }
    }

    public override int GetEnemiesLeft()
    {
        return numberOfEnemiesLeft;
    }
}
