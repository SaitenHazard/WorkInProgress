using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovementModel : CharacterMovementModel
{
    private void Start()
    {
        Debug.Log(m_Attributes);
    }

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
