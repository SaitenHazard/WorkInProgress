using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackablePlayer : Attackable
{
    override protected void OnTriggerEnter2D(Collider2D hitCollider)
    {
        ColliderObject = hitCollider.gameObject;

        if (ColliderObject.tag == "EnemyProjectile" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoHit(1, ColliderObject.GetComponent<Projectile>().GetHitDirection());

            ColliderObject.GetComponent<Projectile>().DestroyOnHit();
        }

        if (ColliderObject.tag == "Enemy" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Attackable attackerAttackable = ColliderObject.transform.parent.gameObject.
                            GetComponentInChildren<Attackable>();

            if (attackerAttackable != null)
            {
                if (attackerAttackable.GetHealth() <= 0)
                    return;

                CharacterMovementModel attackerMovementModel = ColliderObject.GetComponentInParent<CharacterMovementModel>();

                attackerMovementModel.SetTemporaryFrozen(1);
            }

            DoHit(1, attackerMovementModel.GetFacingDirection());
        }

        if (ColliderObject.tag == "Hazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoHit(1, GetComponentInParent<CharacterMovementModel>().GetReverseFacingDirection());
        }
    }
}
