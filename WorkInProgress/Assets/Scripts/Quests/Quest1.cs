using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : QuestBase
{
    private int maxNumberOfEnemies;
    private int numberOfEnemiesLeft;

    public override void Activate()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        for (int i = 0; i < childTransforms.Length; i++)
        {
            childTransforms[i].gameObject.SetActive(true);
        }

        AIBase [] aiScripts = GetComponentsInChildren<AIBase>();

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
