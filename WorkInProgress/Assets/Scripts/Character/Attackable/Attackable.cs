using UnityEngine;
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
    private float stunTime;
    private PlayerStats playerStats;

    private void Awake()
    {
        parentObject = transform.parent.gameObject;
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
        aiBase = transform.parent.GetComponentInChildren<AIBase>();
        spriteRenderer = parentObject.GetComponentInChildren<SpriteRenderer>();
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
        playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();
    }

    private void Start()
    {
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

    protected virtual void OnTriggerEnter2D (Collider2D hitCollider)
    {
        if(aiBase.GetEnemyAction() == enumEnemyActions.defend)
        {
            return;
        }

        ColliderObject = hitCollider.gameObject;

        if (ColliderObject.tag == "Punch" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            float damage = playerStats.GetDamage();

            attackerMovementModel = ColliderObject.GetComponentInParent<CharacterMovementModel>();

            DoHit(damage, attackerMovementModel.GetFacingDirection());
        }

        if(ColliderObject.tag == "PlayerProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            if (health <= 0)
                return;

            float damage = ColliderObject.GetComponent<Projectile>().GetDamage();

            Destroy(ColliderObject.gameObject);

            DoHit(damage, ColliderObject.GetComponent<Projectile>().GetMovementDirection());
        }

        if (ColliderObject.tag == "PlayerHazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            if (health <= 0)
                return;

            float damage = ColliderObject.GetComponent<PlayerSlime>().GetDamage();
            Vector2 movementDirection = transform.parent.GetComponent<CharacterMovementModel>().GetReverseFacingDirection();
            DoHit(damage, movementDirection);
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

        if (health <= 0)
        {
            DoDestroy();
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
    }

    private void DoDestroy()
    {
        StartCoroutine(characterFadeOut());
    }

    public IEnumerator DoStunView(float stunTime)
    {
        Debug.Log("In DoStunView");

        speechBubble.ShowSpeechBubble(enumSpeechBubbles.Paralyze);
        yield return new WaitForSeconds(stunTime);
        speechBubble.HideSpeechBubble();
    }

    private IEnumerator characterFadeOut()
    {
        float opacity = 1f;

        yield return new WaitForSeconds(pushBackTime);

        while (opacity > 0.2f)
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
