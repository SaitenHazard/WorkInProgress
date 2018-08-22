using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModel : CharacterMovementModel
{
    override public void DoAttack()
    {
        SetMovementFrozen(true);
    }
}
