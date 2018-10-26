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
            DoDoHit(1, ColliderObject.GetComponent<Projectile>().GetHitDirection());

            ColliderObject.GetComponent<Projectile>().DestroyOnHit();
        }

        if (ColliderObject.tag == "Enemy" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            Attackable attackerAttackable = ColliderObject.transform.parent.gameObject.
                            GetComponentInChildren<Attackable>();

            if (attackerAttackable.GetHealth() <= 0)
                return;

            CharacterMovementModel attackerMovementModel = ColliderObject.GetComponentInParent<CharacterMovementModel>();

            attackerMovementModel.SetTemporaryFrozen(1);
            DoDoHit(1, attackerMovementModel.GetFacingDirection());
        }

        if (ColliderObject.tag == "Hazard" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            DoDoHit(1, GetComponentInParent<CharacterMovementModel>().GetReverseFacingDirection());
        }
    }

    private void DoDoHit(float damage, Vector2 hitDirection)
    {
        if (playerStats.IsInvincibleUp() == true)
            return;

        DoHit(damage, hitDirection);
    }
}
