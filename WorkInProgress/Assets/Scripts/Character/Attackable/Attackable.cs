using UnityEngine;
using System.Collections;

public class Attackable : MonoBehaviour 
{
    public int health;
    public float pushBackTime;
    public float pushBackSpeed;

    private GameObject parentObject;
    private CharacterMovementModel attackerMovementModel;

    protected GameObject ColliderObject;
    protected CharacterMovementModel m_movementModel;

    private void Awake()
    {
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
        parentObject = transform.parent.gameObject;
    }

    public int GetHealht()
    {
        return health;
    }

    protected virtual void OnTriggerEnter2D (Collider2D hitCollider)
    {
        ColliderObject = hitCollider.gameObject;

        if (ColliderObject.tag == "Punch" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoHit();
        }
    }

    protected void DoHit()
    {
        attackerMovementModel =
                ColliderObject.GetComponentInParent<CharacterMovementModel>();

        m_movementModel.GetHit(attackerMovementModel.GetFacingDirection(),
            pushBackTime, pushBackSpeed);

        SubstractHealth();

        if (health <= 0)
            DoDestroy();
    }

    private void SubstractHealth()
    {
        SubstractHealth(1);
    }

    private void SubstractHealth(int number)
    {
        health -= number;
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
