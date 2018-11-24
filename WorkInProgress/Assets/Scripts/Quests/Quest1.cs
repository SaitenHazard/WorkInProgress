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
        AIBase[] aiScripts = GetComponentsInChildren<AIBase>();
        numberOfEnemiesLeft = aiScripts.Length;
    }

    public int GetEnemiesLeft()
    {
        return numberOfEnemiesLeft;
    }
}
