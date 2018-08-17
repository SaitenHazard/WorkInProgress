using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    private CharacterBaseControl m_baseControl;
    private CharacterAttributes m_attributes;

    private void Awake()
    {
        m_baseControl = gameObject.GetComponentInParent<CharacterBaseControl>();
        m_attributes = gameObject.GetComponentInParent<CharacterAttributes>();
    }

    public override void OnHit(Collider2D hitCollider)
    { 
        if (hitCollider.tag == "punch")
        {
            CharacterAttributes attackerAttributes = hitCollider.gameObject.GetComponentInParent<CharacterAttributes>();
            m_attributes.SetAttackDirection(attackerAttributes.GetFacingDirection());

            m_baseControl.DoHit();
        }
    }
}
