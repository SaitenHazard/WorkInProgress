using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementModel : MonoBehaviour
{
    protected bool stopMovement;
    private float m_speed;

    private Rigidbody2D m_Body;

    private Vector2 m_MovmentDirection;
    private Vector2 m_RecievedDirection;
    private Vector2 m_FacingDirection;

    public void Awake()
    {
        stopMovement = false;

        m_Body = GetComponent<Rigidbody2D>();
        m_speed = gameObject.GetComponent<CharacterAttributes>().getSpeed();
    }

    protected void Update()
    {
        if (stopMovement == true)
        {
            return;
        }

        UpdateDirection();
        ResetRecievedDirection();
    }

    private void FixedUpdate()
    {
        if (stopMovement == true)
        {
            return;
        }

        UpdateMovement();
    }

    private void ResetRecievedDirection()
    {
        m_RecievedDirection = Vector2.zero;
    }

    private void UpdateMovement()
    {
        if(m_MovmentDirection != Vector2.zero)
        {
            m_MovmentDirection.Normalize();
        }

        m_Body.velocity = m_MovmentDirection * m_speed;
    }

    private void UpdateDirection()
    {
        m_MovmentDirection = new Vector2(m_RecievedDirection.x, m_RecievedDirection.y);

        if (stopMovement)
            m_MovmentDirection = Vector2.zero;

        if (m_RecievedDirection != Vector2.zero)
        {
            Vector2 facingDirection = m_MovmentDirection;

            if (facingDirection.x != 0 && facingDirection.y != 0)
            {
                if (facingDirection.x == m_FacingDirection.x)
                {
                    facingDirection.y = 0;
                }
                else if (facingDirection.y == m_FacingDirection.y)
                {
                    facingDirection.x = 0;
                }
                else
                {
                    facingDirection.x = 0;
                }
            }

            m_FacingDirection = facingDirection;
        }
    }

    public Vector2 GetFacingDirection()
    {
        return m_FacingDirection;
    }

    public bool IsMoving()
    {
        return m_MovmentDirection != Vector2.zero;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }

        m_RecievedDirection = direction;
    }

    virtual public void DoAction()
    {

    }

    virtual public void DoAttack()
    {

    }

    virtual public bool GetAttackFlag()
    {
        return false;
    }

    virtual public void SetAttackFlag(bool set)
    {

    }

    virtual public void AttactAnimationListener()
    {

    }
}
