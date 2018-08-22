using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationView : CharacterMovementView
{
    override public void DoAttack()
    {
        Animator.SetTrigger("DoAttack");
    }
}
