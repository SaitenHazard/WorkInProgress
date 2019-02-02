using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableShielderB : Attackable
{
    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (aiBase != null && aiBase.GetEnemyAction() == enumEnemyActions.defend)
        {
            return;
        }

        if (collider2D.tag == "Punch" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            attackerMovementModel = collider2D.GetComponentInParent<CharacterMovementModel>();

            if (attackerMovementModel.GetFacingDirection() == new Vector3 (0,1) || attackerMovementModel.GetFacingDirection() == new Vector3(0, -1))
            {
                return;
            }

            float damage = playerStats.GetDamage();

            DoHit(damage, attackerMovementModel.GetFacingDirection());
        }

        if (collider2D.tag == "PlayerProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            if (collider2D.GetComponent<Projectile>().GetMovementDirection() == new Vector2(0, 1) || 
                collider2D.GetComponent<Projectile>().GetMovementDirection() == new Vector2(0, -1))
            {
                return;
            }

            if (health <= 0)
                return;

            float damage = collider2D.GetComponent<Projectile>().GetDamage();

            Destroy(collider2D.gameObject);

            DoHit(damage, collider2D.GetComponent<Projectile>().GetMovementDirection());
        }

        if (collider2D.tag == "PlayerHazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 movementDirection = transform.parent.GetComponent<CharacterMovementModel>().GetReverseFacingDirection();

            if (movementDirection == new Vector2(0, 1) || movementDirection == new Vector2(0, -1))
            {
                return;
            }

            if (health <= 0)
                return;

            float damage = collider2D.GetComponent<PlayerSlime>().GetDamage();
            DoHit(damage, movementDirection);
        }

        if (collider2D.tag == "PlayerBombRing" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 hitDirection = collider2D.GetComponentInParent<PlayerBomb>().GetHitDirection(transform);

            if (hitDirection == new Vector2(0, -1) || hitDirection == new Vector2(0, -1))
            {
                return;
            }

            if (health <= 0)
                return;

            DoHit(1, hitDirection);
        }
    }
}
