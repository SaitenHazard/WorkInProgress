using System.Collections;
using UnityEngine;

public class BipolarAI : AIBase
{
    private GameObject visualsA;
    private GameObject visualsB;
    private int polarForTime;
    private int polarAfterTime;

    public float speed;
    public float damage;

    private void Awake()
    {
        Transform []temp = transform.parent.GetComponentsInChildren<Transform>();

        visualsA = temp[1].gameObject;
        visualsB = temp[2].gameObject;
    }

    private void Start()
    {
        base.Start();

        polarForTime = Random.Range(3, 6);
        polarAfterTime = Random.Range(5, 9);
    
        StartCoroutine(ClockPolarAfterTime());
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

        if (collider2D.gameObject.tag == "PlayerHitBox")
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

        if (collider2D.gameObject.tag == "PlayerHitBox")
        {
            enemyAction = basicAction;
        }
    }

    private IEnumerator ClockPolarAfterTime()
    {
        polarForTime = Random.Range(3, 6);
        yield return new WaitForSeconds(polarAfterTime);
        StartCoroutine(ClockPolarForTime());
    }

    private IEnumerator ClockPolarForTime()
    {
        polarAfterTime = Random.Range(3, 6);
        yield return new WaitForSeconds(polarForTime);
        StartCoroutine(ClockPolarAfterTime());
    }
}
