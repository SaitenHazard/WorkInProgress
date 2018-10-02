using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAI : AIBase
{
    private int defendForTime;
    private int defendAfterTime;

    private void Start()
    {
        defendForTime = Random.Range(3, 6);
        defendAfterTime = Random.Range(5, 9);

        StartCoroutine(clockDefendAfterTime());
    }

    override protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (enemyActions == enumEnemyActions.defend)
            return;

        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerEnter2D(collider2D);
            enemyActions = enumEnemyActions.chase;
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (enemyActions == enumEnemyActions.defend)
            return;

        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerEnter2D(collider2D);
            enemyActions = enumEnemyActions.chase;
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (enemyActions == enumEnemyActions.defend)
            return;

        if (collider2D.gameObject.tag == "Player")
        {
            SetNullDirection();
            enemyActions = enumEnemyActions.patrol;
        }
    }

    private IEnumerator clockDefendAfterTime()
    {
        enemyActions = enumEnemyActions.NULL;
        yield return new WaitForSeconds(defendAfterTime);
        StartCoroutine(clockDefendForTime());
    }

    private IEnumerator clockDefendForTime()
    {
        enemyActions = enumEnemyActions.defend;
        yield return new WaitForSeconds(defendForTime);
        StartCoroutine(clockDefendAfterTime());
    }
}
