using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModel : CharacterMovementModel
{ 
    private void Update()
    {
        Speed = GetComponent<PlayerStats>().GetSpeed();
        base.Update();
    }

    override public void DoAttack()
    {
        SetMovementFrozen(true);
    }
}
