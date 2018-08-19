using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    public int health;
    public float pushBackTime;

    private CharacterBaseControl m_baseControl;
    private CharacterAttributes m_attributes;

    private void Awake()
    {
        m_baseControl = gameObject.GetComponentInParent<CharacterBaseControl>();
        m_attributes = gameObject.GetComponentInParent<CharacterAttributes>();

        SetAttackable();
    }

    private void SetAttackable()
    {
        m_attributes.SetHealth(health);
        m_attributes.SetPushBackTime(pushBackTime);
    }

    public void OnTriggerEnter2D (Collider2D hitCollider)
    {
        if (hitCollider.gameObject.tag == "Punch")
        {
            CharacterAttributes attackerAttributes = hitCollider.gameObject.GetComponentInParent<CharacterAttributes>();
            m_attributes.SetAttackDirection(attackerAttributes.GetFacingDirection());

            m_baseControl.DoHit();
        }
    }
}
