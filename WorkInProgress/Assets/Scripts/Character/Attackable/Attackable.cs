﻿using UnityEngine;
using System.Collections;

public class Attackable : MonoBehaviour 
{
    public float health;
    public float maxHealth;
    public float pushBackTime;
    public float pushBackSpeed;

    protected CharacterMovementModel attackerMovementModel;
    protected GameObject ColliderObject;
    protected CharacterMovementModel m_movementModel;

    private AIBase aiBase;
    private SpriteRenderer spriteRenderer;
    private GameObject parentObject;
    private Color color;
    private SpeechBubble speechBubble;
    private float shockTime;

    private void Awake()
    {
        maxHealth = health;
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
        parentObject = transform.parent.gameObject;
        aiBase = transform.parent.GetComponentInChildren<AIBase>();
        spriteRenderer = parentObject.GetComponentInChildren<SpriteRenderer>();
        color = spriteRenderer.color;
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
    }

    private void Start()
    {
        shockTime = 10f;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void RestoreFullHealth()
    {
        health = maxHealth;
    }

    protected virtual void OnTriggerEnter2D (Collider2D hitCollider)
    {
        if(aiBase.GetEnemyAction() == enumEnemyActions.defend)
        {
            return;
        }

        ColliderObject = hitCollider.gameObject;

        if (ColliderObject.tag == "Punch" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            int damage = (ColliderObject.GetComponentInParent<PlayerStats>()).GetDamage();

            attackerMovementModel = ColliderObject.GetComponentInParent<CharacterMovementModel>();

            DoHit(damage, attackerMovementModel.GetFacingDirection());

            if(ColliderObject.GetComponentInParent<PlayerStats>().IsParalyzeUp() == true)
            {
                DoParalyze(shockTime);
            }
        }

        if(ColliderObject.tag == "PlayerProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            if (health <= 0)
                return;

            Destroy(ColliderObject.gameObject);

            float damage = ColliderObject.GetComponent<Projectile>().GetDamage();

            DoHit(damage, ColliderObject.GetComponent<Projectile>().GetMovementDirection());

            if (ColliderObject.GetComponentInParent<PlayerStats>().IsParalyzeUp() == true)
            {
                DoParalyze(shockTime);
            }
        }
    }

    protected void DoHit(float damage, Vector2 hitDirection)
    {
        if (health <= 0)
            return;

        m_movementModel.GetHit(hitDirection, pushBackTime, pushBackSpeed);

        SubstractHealth(damage);

        if (ColliderObject.GetComponentInParent<PlayerStats>().IsParalyzeUp() == true)
        {
            DoParalyze(10f);
        }
    }

    public void AddHealth()
    {
        AddHealth(1);
    }

    public void AddHealth(int amount)
    {
        health += amount;

        if (health >= maxHealth) health = maxHealth;
    }

    public void SetHealth(float l_health)
    {
        health = l_health;
    }

    private void SubstractHealth()
    {
        SubstractHealth(1);
    }

    private void SubstractHealth(float amount)
    {
        health -= amount;
    }

    private void DoDestroy()
    {
        StartCoroutine(characterFadeOut());
    }

    public void DoParalyze(float paralyzeTime)
    {
        if (health <= 0)
            return;

        StartCoroutine(DoParalyzeView(paralyzeTime));
        m_movementModel.SetTemporaryFrozen(paralyzeTime);
    }

    private IEnumerator DoParalyzeView(float paralyzeTime)
    {
        speechBubble
        yield return new WaitForSeconds(paralyzeTime);
    }

    private IEnumerator characterFadeOut()
    {
        float opacity = 1f;

        yield return new WaitForSeconds(pushBackTime);

        while (opacity > 0f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        AIBase aiBase = gameObject.transform.parent.GetComponentInChildren<AIBase>();
        GameObject patrolObject = aiBase.GetPatrolObject();

        if (aiBase != null)
            Destroy(patrolObject);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
