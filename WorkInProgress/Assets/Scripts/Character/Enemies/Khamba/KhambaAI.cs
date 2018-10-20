using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhambaAI : AIBase
{
    private void Start()
    {
        base.Start();

        enemyAction = enumEnemyActions.NULL;
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

    protected override void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            enemyAction = enumEnemyActions.NULL;
            StopAllCoroutines();
        }
    }
}
