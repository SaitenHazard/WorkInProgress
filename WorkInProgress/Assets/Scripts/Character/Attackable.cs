using UnityEngine;
using System.Collections;

public class Attackable : MonoBehaviour 
{
    public float health;
    

    protected CharacterMovementModel attackerMovementModel;
    protected CharacterMovementModel m_movementModel;
    protected PlayerStats playerStats;
    protected SpriteRenderer spriteRenderer;
    protected float pushBackTime = 0.15f;

    private float maxHealth;
    private AIBase aiBase;
    private GameObject parentObject;
    private Color color;
    private SpeechBubble speechBubble;
    private float stunTime;
    private float pushBackSpeed = 2.5f;

    virtual protected void Awake()
    {
        parentObject = transform.parent.gameObject;
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
        aiBase = transform.parent.GetComponentInChildren<AIBase>();
        spriteRenderer = parentObject.GetComponentInChildren<SpriteRenderer>();
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
    }

    private void Start()
    {
        playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();

        stunTime = 5f;
        color = spriteRenderer.color;
        maxHealth = health;
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

    protected virtual void OnTriggerEnter2D (Collider2D collider2D)
    {
        if(aiBase != null && aiBase.GetEnemyAction() == enumEnemyActions.defend)
        {
            return;
        }

        if (collider2D.tag == "Punch" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            float damage = playerStats.GetDamage();

            attackerMovementModel = collider2D.GetComponentInParent<CharacterMovementModel>();

            DoHit(damage, attackerMovementModel.GetFacingDirection());
        }

        if(collider2D.tag == "PlayerProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            if (health <= 0)
                return;

            float damage = collider2D.GetComponent<Projectile>().GetDamage();

            Destroy(collider2D.gameObject);

            DoHit(damage, collider2D.GetComponent<Projectile>().GetMovementDirection());
        }

        if (collider2D.tag == "PlayerHazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            if (health <= 0)
                return;

            float damage = collider2D.GetComponent<PlayerSlime>().GetDamage();
            Vector2 movementDirection = transform.parent.GetComponent<CharacterMovementModel>().GetReverseFacingDirection();
            DoHit(damage, movementDirection);
        }

        if (collider2D.tag == "PlayerBombRing" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            if (health <= 0)
                return;

            Vector2 hitDirection = collider2D.GetComponentInParent<PlayerBomb>().GetHitDirection(transform);

            DoHit(1, hitDirection);
        }
    }

    protected void DoHit(float damage, Vector2 hitDirection)
    {
        if (health <= 0)
            return;

        SubstractHealth(damage);

        if (playerStats.IsStunUp() == true)
        {
            StartCoroutine(DoStunView(stunTime));
            m_movementModel.GetHit(hitDirection, pushBackTime, pushBackSpeed, stunTime);
        }
        else
        {
            m_movementModel.GetHit(hitDirection, pushBackTime, pushBackSpeed);
        }
    }

    public void AddHealth()
    {
        AddHealth(1);
    }

    public void AddHealth(float amount)
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

    public void SubstractHealth(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            DoDestroy();
        }
    }

    virtual protected void DoDestroy()
    {
        aiBase = gameObject.transform.parent.GetComponentInChildren<AIBase>();

        GameObject patrolObject = aiBase.GetPatrolObject();

        if (aiBase != null)
            Destroy(patrolObject);

        SpawnManager spawnManager = GetComponentInParent<SpawnManager>();

        if(spawnManager != null)
        {
            AIBase aiBase = spawnManager.GetSpanwerAI();

            if (aiBase != null)
                aiBase.DeductSpawn();
        }

        GameObject temp = Instantiate(Resources.Load("EnemyDestroy")) as GameObject;

        temp.transform.position = transform.position;

        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        Destroy(gameObject.transform.parent.gameObject);
    }

    public IEnumerator DoStunView(float stunTime)
    {
        speechBubble.ShowSpeechBubble(enumSpeechBubbles.Paralyze);
        yield return new WaitForSeconds(stunTime);
        speechBubble.HideSpeechBubble();
    }

    protected virtual IEnumerator CharacterFadeOut()
    {
        float opacity = 1f;

        yield return new WaitForSeconds(pushBackTime);

        while (opacity > 0.4f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

    }
}
