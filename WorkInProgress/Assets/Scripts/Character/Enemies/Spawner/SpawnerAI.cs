using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAI : AIBase
{
    private void Start()
    {
        base.Start();
        enemyAction = enumEnemyActions.idle;
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Decoy")
        {
            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction != enumEnemyActions.spawn)
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.spawn;

            return;
        }

        if (collider2D.gameObject.tag == "Player")
        {
            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction != enumEnemyActions.spawn)
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.spawn;
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            enemyAction = enumEnemyActions.idle;
        }
    }
}
