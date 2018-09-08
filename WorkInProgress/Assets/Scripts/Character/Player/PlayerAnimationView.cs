using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationView : CharacterMovementView
{
    protected void Update()
    {
        base.Update();
        UpdateAttact2();
    }

    private void UpdateAttact2()
    {
        bool attack2 = (PlayerInstant.Instance.GetComponent<PlayerStats>()).IsDamageUp();

        Animator.SetBool("Attack2", attack2);
    }

    override public void DoAttack()
    {
        Animator.SetBool("DoAttack", true);
    }
}
