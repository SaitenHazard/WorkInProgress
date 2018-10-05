using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAI : AIBase
{
    private int defendForTime;
    private int defendAfterTime;

    private void Start()
    {
        base.Start();

        defendForTime = Random.Range(3, 6);
        defendAfterTime = Random.Range(5, 9);

        //StartCoroutine(clockDefendAfterTime());
    }

    override protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        //if (collider2D.gameObject.tag == "Player")
        //{
        //    base.OnTriggerEnter2D(collider2D);
        //    enemyAction = enumEnemyActions.chase;
        //    target = PlayerInstant.Instance.GetComponent<Transform>();
        //}
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        //if (collider2D.gameObject.tag == "Player")
        //{
        //    base.OnTriggerStay2D(collider2D);
        //    enemyAction = enumEnemyActions.chase;
        //    target = PlayerInstant.Instance.GetComponent<Transform>();
        //}
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        //if (collider2D.gameObject.tag == "Player")
        //{
        //    SetNullDirection();
        //    enemyAction = enumEnemyActions.patrol;
        //}
    }

    private IEnumerator clockDefendAfterTime()
    {
        enemyAction = enumEnemyActions.patrol;
        yield return new WaitForSeconds(defendAfterTime);
        StartCoroutine(clockDefendForTime());
    }

    private IEnumerator clockDefendForTime()
    {
        enemyAction = enumEnemyActions.defend;
        yield return new WaitForSeconds(defendForTime);
        StartCoroutine(clockDefendAfterTime());
    }
}
