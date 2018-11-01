using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShroomAI : AIBase
{
    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();

        RevertHealAlly();
    }

    private void RevertHealAlly()
    {
        if(injuredAlly == null)
        {
            enemyAction = enumEnemyActions.patrol;
        }
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Decoy")
        {
            if (enemyAction != enumEnemyActions.chaseDecoy)
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.chaseDecoy;
            target = collider2D.gameObject.transform;

            return;
        }

        if (enemyAction == enumEnemyActions.healAlly)
            return;

        if (collider2D.gameObject.tag == "Enemy")
        {
            Attackable attackable = collider2D.transform.parent.GetComponentInChildren<Attackable>();

            if (attackable.GetHealth() < attackable.GetMaxHealth())
            {
                enemyAction = enumEnemyActions.healAlly;
                injuredAlly = collider2D.transform.parent;
            }
        }

        if (collider2D.gameObject.tag == "Player")
        {
            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction != enumEnemyActions.chase)
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction != enumEnemyActions.healAlly)
            {
                enemyAction = enumEnemyActions.chase;
                target = PlayerInstant.Instance.GetComponent<Transform>();
            }
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (enemyAction == enumEnemyActions.healAlly)
            return;

        if (collider2D.gameObject.tag == "Player")
        {
            enemyAction = enumEnemyActions.patrol;
        }
    }
}
