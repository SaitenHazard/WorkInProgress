﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseControl : MonoBehaviour
{
    protected CharacterMovementModel m_movementModel;
    protected CharacterMovementView m_movementView;
    protected CharacterAttributes m_attributes;

    protected void Awake()
    {
        m_movementModel = gameObject.GetComponent<CharacterMovementModel>();
        m_movementView = gameObject.GetComponent<CharacterMovementView>();
        m_attributes = gameObject.GetComponent<CharacterAttributes>();
    }

    protected Vector2 GetDiagonalizedDirection(Vector2 direction, float threshold)
    {

        if(Mathf.Abs(direction.x) < threshold)
        {
            direction.x = 0;
        }
        else
        {
            direction.x = Mathf.Sign(direction.x);
        }

        if(Mathf.Abs(direction.y) < threshold)
        {
            direction.y = Mathf.Sign(direction.y);
        }
        else
        {
            direction.y = Mathf.Sign(direction.y);
        }

        return direction;
    }

    public void SetDirection(Vector2 direction)
    {
        if (m_movementModel == null)
        {
            return;
        }

        m_movementModel.SetDirection(direction);
    }

    public void Hit()
    {
        m_attributes.SetHitState(true);
        m_attributes.setWalkStateFrozen(false);
        m_attributes.SetAttackState(false);
    }
}
