using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAI : AIBase
{
    override protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerEnter2D(collider2D);
            enemyAction = enumEnemyActions.chase;
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            SetNullDirection();
            enemyAction = enumEnemyActions.patrol;
        }
    }
}
