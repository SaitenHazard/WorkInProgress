﻿using System.Collections;
using UnityEngine;

public class BipolarAI : AIBase
{
    private int polarForTime;
    private int polarAfterTime;
    private int damage;
    private float speed;
    private Animator animator;

    public float speedPolar;
    public int damagePolar;
    public RuntimeAnimatorController visualsA;
    public RuntimeAnimatorController visualsB;

    private void Awake()
    {
        base.Awake();
        Transform []temp = transform.parent.GetComponentsInChildren<Transform>();
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        base.Start();

        polarForTime = Random.Range(3, 6);
        polarAfterTime = Random.Range(5, 9);

        damage = m_attackable.GetDamage();
        speed = m_movementModel.GetSpeed();
    
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
        SetPolar(false);
        yield return new WaitForSeconds(polarAfterTime);
        StartCoroutine(ClockPolarForTime());
    }

    private IEnumerator ClockPolarForTime()
    {
        polarAfterTime = Random.Range(3, 6);
        SetPolar(true);
        yield return new WaitForSeconds(polarForTime);
        StartCoroutine(ClockPolarAfterTime());
    }

    private void SetPolar(bool polar)
    {
        if (polar == true)
        {
            m_movementModel.SetSpeed(speedPolar);
            m_attackable.SetDamage(damagePolar);
            animator.runtimeAnimatorController = visualsB;
        }
        else
        {
            m_movementModel.SetSpeed(speed);
            m_attackable.SetDamage(damage);
            animator.runtimeAnimatorController = visualsA;
        }
    }
}
