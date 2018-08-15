using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementView : CharacterMovementView
{
    override protected void Update()
    {
        base.Update();
        UpdateAttack();
    }
    
    private void UpdateAttack()
    {
        if (m_MovementModel.GetAttackFlag() == true)
        {
            m_MovementModel.SetAttackFlag(false);
            animator.SetTrigger("Attack");
        }
    }
}
