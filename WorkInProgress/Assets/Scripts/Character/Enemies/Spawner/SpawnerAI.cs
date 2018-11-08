using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAI : AIBase
{
    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Decoy")
        {
            if (enemyAction == basicActionWithPlayer)
                return;

            speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.spawn;
            target = collider2D.GetComponent<Transform>();
        }

        if (collider2D.gameObject.tag == "Player")
        {
            playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();

            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction == basicActionWithPlayer)
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
