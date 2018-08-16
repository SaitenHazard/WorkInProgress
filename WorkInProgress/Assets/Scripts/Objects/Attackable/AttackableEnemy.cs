using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    private CharacterMovementModel m_MovementModel;

    private void Awake()
    {
        m_MovementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
    }

    public override void OnHit( Collider2D hitCollider)
    { 
        if (hitCollider.tag == "punch")
        {
            
        }
    }
}
