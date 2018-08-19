using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseControl : MonoBehaviour
{
    public float walkSpeed;

    protected CharacterMovementModel m_movementModel;
    protected CharacterMovementView m_movementView;
    protected CharacterAttributes m_attributes;

    protected void Awake()
    {
        m_movementModel = gameObject.GetComponent<CharacterMovementModel>();
        m_movementView = gameObject.GetComponent<CharacterMovementView>();
        m_attributes = gameObject.GetComponent<CharacterAttributes>();
        m_attributes.SetSpeed(walkSpeed);
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

    public void DoHit()
    {
        m_attributes.SetHitState(true);
        m_movementView.DoHit();
    }

    virtual public void UpdateDirection(Directions dir)
    {
        Vector2 newDirection = Vector2.zero;

        if (dir == Directions.Up)
        {
            newDirection.y = 1;
        }

        if (dir == Directions.Down)
        {
            newDirection.y = -1;
        }

        if (dir == Directions.Left)
        {
            newDirection.x = -1;
        }

        if (dir == Directions.Right)
        {
            newDirection.x = 1;
        }

        SetDirection(newDirection);
    }

    virtual public void OnActionPressed()
    {

    }
}
