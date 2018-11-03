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

        if (ColliderObject.tag == "BombRing" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            float angle = Mathf.Atan2 
                (ColliderObject.transform.position.y - gameObject.transform.position.y, 
                ColliderObject.transform.position.x - gameObject.transform.position.y) 
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
    }

    private void DoDoHit(float damage, Vector2 hitDirection)
    {
        if (playerStats.IsInvincibleUp() == true)
            return;

        DoHit(damage, hitDirection);
    }
}
