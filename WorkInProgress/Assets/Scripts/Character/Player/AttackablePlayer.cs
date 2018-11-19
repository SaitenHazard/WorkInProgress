using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackablePlayer : Attackable
{
    override protected void Start()
    {
        base.Start();
        health = 10;
        SetMaxHealth(10);
    }

    override protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "EnemyProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoDoHit(1, collider2D.GetComponent<Projectile>().GetHitDirection());

            collider2D.GetComponent<Projectile>().DestroyOnHit();
        }

        if (collider2D.tag == "Enemy" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Attackable attackerAttackable = collider2D.transform.parent.gameObject.
                            GetComponentInChildren<Attackable>();

            if(attackerAttackable != null)
            {
                if (attackerAttackable.GetHealth() <= 0)
                    return;
            }

            CharacterMovementModel attackerMovementModel = collider2D.GetComponentInParent<CharacterMovementModel>();

            attackerMovementModel.SetTemporaryFrozen(1);
            DoDoHit(1, attackerMovementModel.GetFacingDirection());
        }

        if (collider2D.tag == "Hazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoDoHit(1, GetComponentInParent<CharacterMovementModel>().GetReverseFacingDirection());
        }

        if (collider2D.tag == "BombRing" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoBombHit(collider2D);
        }
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.tag == "BombRing" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoBombHit(collider2D);
        }
    }

    private void DoBombHit(Collider2D collider2D)
    {
        float angle = Mathf.Atan2
                (collider2D.transform.position.y - gameObject.transform.position.y,
                collider2D.transform.position.x - gameObject.transform.position.y)
                * 180 / Mathf.PI * -1;

        Vector2 hitDirection = Vector2.zero;

        if (angle >= 45 && angle <= 135)
        {
            hitDirection = new Vector2(0, 1);
        }

        if (angle <= -45 && angle >= -135)
        {
            hitDirection = new Vector2(0, -1);
        }

        if (angle >= 0 && angle < 45 || angle < 0 && angle > -45)
        {
            hitDirection = new Vector2(-1, 0);
        }

        if (angle > 135 && angle <= 180 || angle < -135 && angle >= -180)
        {
            hitDirection = new Vector2(1, 0);
        }

        DoDoHit(1, hitDirection);
    }

    private void DoDoHit(float damage, Vector2 hitDirection)
    {
        if (playerStats.IsInvincibleUp() == true)
            return;

        DoHit(damage, hitDirection);
    }
}
