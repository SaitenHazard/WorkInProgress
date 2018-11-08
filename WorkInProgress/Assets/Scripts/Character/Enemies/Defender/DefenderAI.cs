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
    
        StartCoroutine(ClockDefendAfterTime());
    }

    override protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (enemyAction == enumEnemyActions.defend)
            return;

            if (collider2D.gameObject.tag == "Decoy")
        {
            if (enemyAction == enumEnemyActions.chaseDecoy)
                return;

            speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);

            enemyAction = enumEnemyActions.chaseDecoy;
            target = collider2D.GetComponent<Transform>();
        }

        if (collider2D.gameObject.tag == "Player")
        {
            playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();

            if (playerStats.IsInvisibleUp() == true)
                return;

            if (enemyAction == enumEnemyActions.chaseDecoy ||
                enemyAction == enumEnemyActions.chase)
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

    private IEnumerator ClockDefendAfterTime()
    {
        defendForTime = Random.Range(3, 6);
        enemyAction = basicAction;
        yield return new WaitForSeconds(defendAfterTime);
        StartCoroutine(ClockDefendForTime());
    }

    private IEnumerator ClockDefendForTime()
    {
        defendAfterTime = Random.Range(3, 6);
        enemyAction = enumEnemyActions.defend;
        yield return new WaitForSeconds(defendForTime);
        StartCoroutine(ClockDefendAfterTime());
    }
}
