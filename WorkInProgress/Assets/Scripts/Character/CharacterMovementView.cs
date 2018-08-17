﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementView : MonoBehaviour
{
    protected Animator animator;
    protected CharacterMovementModel m_MovementModel;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    virtual protected void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        Vector2 direction = m_MovementModel.GetFacingDirection();

        if(direction != Vector2.zero)
        {
            if(direction.x != 1 || direction.y!=1)
            {
                animator.SetFloat("X", direction.x);
                animator.SetFloat("Y", direction.y);
            }
        }

        animator.SetBool("Walk", m_MovementModel.IsMoving());
    }

    virtual public void DoAttack()
    {

    }

    virtual public void DoHit()
    {
        animator.SetBool("Hit", true);
    }
}
