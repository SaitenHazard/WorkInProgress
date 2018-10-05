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

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerEnter2D(collider2D);
            enemyAction = enumEnemyActions.chase;
            StartCoroutine(ProjectileInstantiate());
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
