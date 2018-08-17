using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovementModel : CharacterMovementModel
{
    protected void Update()
    {
        if (PlayerAttributes.instance.IsGameStateFrozen())
            return;

        //this Returns the correct component of the corrent game object.
        Debug.Log(m_Attributes);

        //this is a function from the base class but does not have the reference to m_Attributes
        UpdateDirection();
        ResetRecievedDirection();
    }

    protected void FixedUpdate()
    {
        UpdateMovement();
    }
}
