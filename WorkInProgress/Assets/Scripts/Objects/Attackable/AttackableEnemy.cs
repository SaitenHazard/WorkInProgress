using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    public int health;
    public float pushBackTime;
    public float pushBackSpeed;

    private CharacterMovementModel m_movementModel;

    private void Awake()
    {
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
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

            HealthManager();
        }
    }

    private void HealthManager()
    {
        health -= 1;

        if (health == 0)
            Destroy(gameObject.transform.parent.gameObject);
    }
}
