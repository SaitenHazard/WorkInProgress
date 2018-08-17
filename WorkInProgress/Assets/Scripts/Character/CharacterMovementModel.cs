using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementModel : MonoBehaviour
{
    protected float m_speed;

    protected Rigidbody2D m_Body;
    protected CharacterAttributes m_Attributes;

    protected Vector2 m_MovmentDirection;
    private Vector2 m_RecievedDirection;
    private Vector2 m_FacingDirection;

    virtual protected void Awake()
    {
        Debug.Log("in");
        m_Attributes = GetComponent<CharacterAttributes>();
        m_Body = GetComponent<Rigidbody2D>();
        m_speed = m_Attributes.getSpeed();
    }

    virtual protected void Start()
    {

    }

    protected void ResetRecievedDirection()
    {
        m_RecievedDirection = Vector2.zero;
    }

    virtual protected void UpdateMovement()
    {
        if (PlayerAttributes.instance.IsGameStateFrozen())
        {
            m_MovmentDirection = Vector2.zero;
        }

        if (m_MovmentDirection != Vector2.zero)
        {
            m_MovmentDirection.Normalize();
        }

        m_Body.velocity = m_MovmentDirection * m_speed;
    }

    protected void UpdateDirection()
    {
        m_MovmentDirection = new Vector2(m_RecievedDirection.x, m_RecievedDirection.y);

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

            //Debug.Log(m_Attributes);

            //if (m_Attributes.IsHitState())
                //m_FacingDirection = m_Attributes.GetAttackDirection();

            //m_FacingDirection = facingDirection;
            //m_Attributes.SetDirections(facingDirection);
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

    public virtual void DoAction()
    {

    }

    public void GetHit()
    {
        
    }
}
