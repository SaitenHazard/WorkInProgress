using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    public int health;
    public float pushBackTime;
    public float pushBackSpeed;

    private GameObject parentObject;
    private CharacterMovementModel m_movementModel;

    private void Awake()
    {
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
        parentObject = transform.parent.gameObject;
    }

    public int GetHealht()
    {
        return health;
    }

    private void OnTriggerEnter2D (Collider2D hitCollider)
    {
        HitManager(hitCollider);
    }

    private void HitManager(Collider2D hitCollider)
    {
        GameObject Object = hitCollider.gameObject;

        if (Object.tag == "Punch" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            CharacterMovementModel attackerModel =
                Object.GetComponentInParent<CharacterMovementModel>();

            m_movementModel.GetHit(attackerModel.GetFacingDirection(),
                pushBackTime, pushBackSpeed);

            DeductHealth();

            if(health <= 0)
                DoDestroy();
        }
    }

    private void DeductHealth()
    {
        DeductHealth(1);
    }

    private void DeductHealth(int deductor)
    {
        health -= deductor;
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

        while (color.a > 0f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            color.a = opacity;
            yield return new WaitForSeconds(0.2f);
        }

        DestroyCharacter();
    }

    private void DestroyCharacter()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
