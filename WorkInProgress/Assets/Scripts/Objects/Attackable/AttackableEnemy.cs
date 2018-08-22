using UnityEngine;
using System.Collections;

public class AttackableEnemy : AttackableBase 
{
    public int health;
    public float pushBackTime;

    private CharacterMovementModel m_movementModel;

    private void Awake()
    {
        m_movementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
    }

    public void OnTriggerEnter2D (Collider2D hitCollider)
    {
        GameObject Object = hitCollider.gameObject;

        if (Object.tag == "Punch")
        {
            CharacterMovementModel attackerModel = 
                Object.GetComponentInParent<CharacterMovementModel>();

            m_movementModel.GetHit(attackerModel.GetFacingDirection(), pushBackTime);
        }
    }
}
