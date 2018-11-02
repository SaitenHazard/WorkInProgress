using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShroomAI : AIBase
{
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
            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction != enumEnemyActions.chase)
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction != enumEnemyActions.healAlly)
            {
                enemyAction = enumEnemyActions.chase;
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
