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
           m_movementModel.DoPushBack(attackerMovementModel.GetFacingDirection(), pushBackTime, pushBackSpeed);
        }

        if (collider2D.tag == "PlayerProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 hitDirection = collider2D.GetComponent<Projectile>().GetMovementDirection();
            m_movementModel.DoPushBack(hitDirection, pushBackTime, pushBackSpeed);            
        }

        if (collider2D.tag == "PlayerHazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 movementDirection = transform.parent.GetComponent<CharacterMovementModel>().GetReverseFacingDirection();
            m_movementModel.DoPushBack(movementDirection, pushBackTime, pushBackSpeed);
        }

        if (collider2D.tag == "PlayerBombRing" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Vector2 hitDirection = collider2D.GetComponentInParent<PlayerBomb>().GetHitDirection(transform);
            m_movementModel.DoPushBack(hitDirection, pushBackTime, pushBackSpeed);
        }
    }
}
