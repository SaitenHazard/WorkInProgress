using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableDuoB : Attackable
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
            DoPushBack(attackerMovementModel.GetFacingDirection());
        }

        if (collider2D.tag == "PlayerProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 hitDirection = collider2D.GetComponent<Projectile>().GetMovementDirection();
            DoPushBack(hitDirection);
        }

        if (collider2D.tag == "PlayerHazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 movementDirection = transform.parent.GetComponent<CharacterMovementModel>().GetReverseFacingDirection();
            DoPushBack(movementDirection);
        }

        if (collider2D.tag == "PlayerBombRing" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 hitDirection = collider2D.GetComponentInParent<PlayerBomb>().GetHitDirection(transform);
            DoPushBack(hitDirection);
        }
    }
}
