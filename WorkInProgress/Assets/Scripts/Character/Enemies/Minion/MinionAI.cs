using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAI : AIBase
{
    protected override void Start()
    {
        base.Start();
        enemyAction = enumEnemyActions.pacing;
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction != enumEnemyActions.chase)
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.chase;
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            enemyAction = enumEnemyActions.pacing;
        }
    }
}
