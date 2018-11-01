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
    
        StartCoroutine(clockDefendAfterTime());
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Decoy")
        {
            if (enemyAction != enumEnemyActions.chaseDecoy)
                speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.chaseDecoy;
            target = PlayerInstant.Instance.GetComponent<Transform>();

            return;
        }

        if (playerStats.IsInvisibleUp() == true)
            return;

        if (enemyAction != enumEnemyActions.chase)
            speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

        if (collider2D.gameObject.tag == "Player")
        {
            if(enemyAction != enumEnemyActions.defend)
            {
                enemyAction = enumEnemyActions.chase;
                target = PlayerInstant.Instance.GetComponent<Transform>();
            }
        }
    }

    override protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            if (enemyAction != enumEnemyActions.defend)
            {
                enemyAction = enumEnemyActions.patrol;
                target = PlayerInstant.Instance.GetComponent<Transform>();
            }
        }
    }

    private IEnumerator clockDefendAfterTime()
    {
        defendForTime = Random.Range(3, 6);
        enemyAction = enumEnemyActions.patrol;
        yield return new WaitForSeconds(defendAfterTime);
        StartCoroutine(clockDefendForTime());
    }

    private IEnumerator clockDefendForTime()
    {
        defendAfterTime = Random.Range(3, 6);
        enemyAction = enumEnemyActions.defend;
        yield return new WaitForSeconds(defendForTime);
        StartCoroutine(clockDefendAfterTime());
    }
}
