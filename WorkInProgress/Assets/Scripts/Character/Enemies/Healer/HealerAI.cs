using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAI : AIBase
{
    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Decoy")
        {
            if (enemyAction == enumEnemyActions.chaseDecoy || enemyAction == enumEnemyActions.healAlly)
                return;

            speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.chaseDecoy;
            target = collider2D.GetComponent<Transform>();
        }

        if (collider2D.gameObject.tag == "Enemy")
        {
            if (enemyAction == enumEnemyActions.healAlly)
                return;

            Attackable attackable = collider2D.transform.parent.GetComponentInChildren<Attackable>();

            if (attackable.GetHealth() < attackable.GetMaxHealth())
            {
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

                enemyAction = enumEnemyActions.healAlly;
                target = collider2D.transform.parent;
            }
        }

        if (collider2D.gameObject.tag == "Player")
        {
            playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();

            if (enemyAction == enumEnemyActions.chaseDecoy ||
                enemyAction == enumEnemyActions.healAlly || enemyAction == enumEnemyActions.chase)
                return;

            if (playerStats.IsInvisibleUp() == true)
                return;

            speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = basicActionWithPlayer;
            target = PlayerInstant.Instance.transform;
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (enemyAction == enumEnemyActions.chaseDecoy)
            return;

        if (collider2D.gameObject.tag == "Player")
        {
            enemyAction = basicAction;
        }
    }
}
