using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovementModel : CharacterMovementModel
{
    protected void Update()
    {
        if (PlayerAttributes.instance.IsGameStateFrozen())
            return;

        UpdateDirection();
        ResetRecievedDirection();
    }

    protected void FixedUpdate()
    {
        UpdateMovement();
    }
}
