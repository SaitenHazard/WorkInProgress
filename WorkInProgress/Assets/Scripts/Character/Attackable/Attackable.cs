using UnityEngine;
using System.Collections;

public class Attackable : MonoBehaviour 
{
    public int health;
    public int maxHealth;
    public float pushBackTime;
    public float pushBackSpeed;

    private GameObject parentObject;
    protected CharacterMovementModel attackerMovementModel;

    protected GameObject ColliderObject;
    protected CharacterMovementModel m_movementModel;

    private void Awake()
    {
        maxHealth = health;
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
        parentObject = transform.parent.gameObject;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void RestoreFullHealth()
    {
        health = maxHealth;
    }

    protected virtual void OnTriggerEnter2D (Collider2D hitCollider)
    {
        ColliderObject = hitCollider.gameObject;

        if (ColliderObject.tag == "Punch" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            int damage = (ColliderObject.GetComponentInParent<PlayerStats>()).GetDamage();

            attackerMovementModel =
                ColliderObject.GetComponentInParent<CharacterMovementModel>();

            DoHit(damage, attackerMovementModel.GetFacingDirection());
        }
    }

    protected void DoHit(int damage, Vector2 hitDirection)
    {
        if (health <= 0)
            return;

        m_movementModel.GetHit(hitDirection, pushBackTime, pushBackSpeed);

        SubstractHealth(damage);

        if (health <= 0)
            DoDestroy();
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

    private void SubstractHealth()
    {
        SubstractHealth(1);
    }

    private void SubstractHealth(int amount)
    {
        health -= amount;
    }

    private void DoDestroy()
    {
        StartCoroutine(characterFadeOut());
    }

    private IEnumerator characterFadeOut()
    {
        SpriteRenderer spriteRenderer = parentObject.GetComponentInChildren<SpriteRenderer>();
        Color color = spriteRenderer.color;
        float opacity = 1f;

        yield return new WaitForSeconds(pushBackTime);

        while (opacity > 0f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        AIBase aiBase = gameObject.transform.parent.GetComponentInChildren<AIBase>();
        Debug.Log(aiBase);
        GameObject patrolObject = aiBase.GetPatrolObject();
        Debug.Log(patrolObject);

        if (aiBase != null)
            Destroy(patrolObject);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
