using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    private CharacterBaseControl m_baseControl;

    private void Awake()
    {
        m_baseControl = gameObject.GetComponentInParent<CharacterBaseControl>();
    }

    public override void OnHit( Collider2D hitCollider)
    { 
        if (hitCollider.tag == "punch")
        {
            m_baseControl.DoHit();
        }
    }
}
