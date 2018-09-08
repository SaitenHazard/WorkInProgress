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
            ColliderObject.GetComponent<Projectile>().DestroyOnHit();
            CommonTrigger(ColliderObject);
        }

        if (ColliderObject.tag == "Enemy" && m_movementModel.GetPushBackSpeed() == 0f)
        {
            CommonTrigger(ColliderObject);
        }
    }

    private void CommonTrigger(GameObject ColliderObject)
    {
        Attackable attackerAttackable = ColliderObject.transform.parent.gameObject.
                GetComponentInChildren<Attackable>();
        
        if(attackerAttackable != null)
            if (attackerAttackable.GetHealth() <= 0)
                return;

        CharacterMovementModel attackerMovementModel = ColliderObject.GetComponentInParent<CharacterMovementModel>();

        attackerMovementModel.SetTemporaryFrozen(1);

        DoHit(1);
    }
}
