using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMovementModel : CharacterMovementModel
{
    public void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
        m_speed = gameObject.GetComponent<CharacterAttributes>().getSpeed();
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

    override protected void UpdateMovement()
    {
        base.UpdateMovement();
    }
}
