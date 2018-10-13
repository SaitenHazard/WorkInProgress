using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShroomAI : AIBase
{
    private void Start()
    {
        base.Start();
        Debug.Log(enemyColliders.Count);
    }

    private void Update()
    {
        base.Update();
        RevertHealAlly();
        SearchForInjuredEnemy();
    }

    private void RevertHealAlly()
    {
        if (enemyColliders.Count == 0)
            return;

        if (enemyColliders[enemyColliderIndex].gameObject == null)
        {
            enemyAction = enumEnemyActions.patrol;
            return;
        }

        Attackable attackable = 
            enemyColliders[enemyColliderIndex].transform.parent.
            GetComponentInChildren<Attackable>();

        if (attackable.GetHealth() == attackable.GetMaxHealth())
        {
            enemyAction = enumEnemyActions.patrol;
        }
    }

    private void SearchForInjuredEnemy()
    {
        if (enemyColliders.Count == 0)
        {
            return;
        }

        if (enemyAction == enumEnemyActions.healAlly)
        {
            return;
        }

        enemyColliderIndex = 0;

        while (enemyColliderIndex < enemyColliders.Count)
        {
            Debug.Log("Index: " + enemyColliderIndex);
            Debug.Log("Array: " + enemyColliders);
            Debug.Log(enemyColliderIndex + " : " + enemyColliders.Count);

            //Collider2D temp = enemyColliders[enemyColliderIndex];

            //if (enemyColliders[enemyColliderIndex] == null)
            //    return;

            //Attackable attackable = enemyColliders[enemyColliderIndex].transform.
            //    parent.GetComponentInChildren<Attackable>();

            //if(attackable.GetHealth() < attackable.GetMaxHealth())
            //{
            //    Debug.Log("in");
            //    enemyAction = enumEnemyActions.healAlly;
            //    break;
            //}

            //enemyColliderIndex++;
        }
    }

    override protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Enemy")
        {
            return;
            Debug.Log(collider2D.transform.parent.name);
            enemyColliders.Add(collider2D);
        }

        if (enemyAction == enumEnemyActions.healAlly)
            return;

        if (collider2D.gameObject.tag == "Player")
        {
            speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);
            enemyAction = enumEnemyActions.chase;
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            if (enemyAction != enumEnemyActions.healAlly)
            {
                enemyAction = enumEnemyActions.chase;
                target = PlayerInstant.Instance.GetComponent<Transform>();
            }
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Enemy")
        {
            enemyColliders.Remove(collider2D);
        }

        if (enemyAction == enumEnemyActions.healAlly)
            return;

        if (collider2D.gameObject.tag == "Player")
        {
            enemyAction = enumEnemyActions.patrol;
        }
    }
}
