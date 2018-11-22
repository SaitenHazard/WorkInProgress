using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : QuestBase
{
    private int maxNumberOfEnemies;
    private int numberOfEnemiesLeft;

    public override void Activate()
    {
        GameObject [] gameObjects = transform.

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObject.SetActive(true);
        }

        AIBase [] aiScripts = GetComponentsInChildren<AIBase>();

        maxNumberOfEnemies = aiScripts.Length;
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
