﻿using UnityEngine;
using System.Collections;

public class CharacterMovementView : MonoBehaviour
{
    protected Animator Animator;
    protected CharacterMovementModel m_MovementModel;

    protected void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected void Update()
    {
        UpdateDirection();
        UpdateHit();
    }

    protected void UpdateHit()
    {
        Animator.SetBool("GetHit", m_MovementModel.GetPushBackSpeed() != -1f && m_MovementModel.GetPushBackSpeed() != 0f);
    }

    protected void UpdateDirection()
    {
        Vector3 direction = m_MovementModel.GetFacingDirection();

        if (direction != Vector3.zero)
        {
            if (direction.x != 1 || direction.y != 1)
            {
                Animator.SetFloat("DirectionX", direction.x);
                Animator.SetFloat("DirectionY", direction.y);
            }
        }

        Animator.SetBool("IsMoving", m_MovementModel.IsMoving());
    }

    virtual public void DoAttack()
    {
        Animator.SetTrigger("DoAttack");
    }
}