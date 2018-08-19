using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementModel : MonoBehaviour
{
    private float m_speed;
    private float pushBackSpeed = 3f;

    private Rigidbody2D m_Body;
    private CharacterAttributes m_Attributes;

    private Vector2 m_MovmentDirection;
    private Vector2 m_RecievedDirection;
    public Vector2 m_FacingDirection;

    private bool doPushBack = false;

    protected void Awake()
    {
        m_Attributes = GetComponent<CharacterAttributes>();
        m_Body = GetComponent<Rigidbody2D>();
        m_speed = m_Attributes.getSpeed();
    }

    private void Update()
    {
        UpdateHit();
        UpdateDirection();
        UpdateMovement();
        ResetRecievedDirection();
    }

    protected void UpdateHit()
    {
        if (m_Attributes.IsHitState())
        {
            doPushBack = true;
            StartCoroutine(pushBack());
            m_Attributes.SetHitState(false);
        }
    }

    public bool IsPushBack()
    {
        return doPushBack;
    }

    private IEnumerator pushBack()
    {
        for (float time = Time.time; (Time.time - time) <= m_Attributes.GetPushBackTime();)
        {
            yield return null;
        }

        doPushBack = false;
    }

    private void SetPushBackDirection()
    {
        Vector2 facingDirection = new Vector2();

        if (doPushBack)
        {
            m_MovmentDirection = m_Attributes.GetAttackDirection();
            Debug.Log(m_MovmentDirection);

            if (m_MovmentDirection.x == 1)
            {
                facingDirection.x = -1;
            }
            else if (m_MovmentDirection.x == -1)
            {
                facingDirection.x = 1;
            }
            else if (m_MovmentDirection.y == 1)
            {
                facingDirection.y = -1;
            }
            else
            {
                facingDirection.y = 1;
            }
        }

        m_FacingDirection = facingDirection;
    }

    private void SetMovementDirection()
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

                m_FacingDirection = facingDirection;
            }
        }
    }

    private void UpdateDirection()
    {
        SetMovementDirection();

        if (doPushBack == true)
            SetPushBackDirection();

        m_Attributes.SetDirections(m_FacingDirection);
    }

    protected void ResetRecievedDirection()
    {
        m_RecievedDirection = Vector2.zero;
    }

    protected void UpdateMovement()
    {
        if (PlayerAttributes.instance.IsGameStateFrozen()
            || m_Attributes.IsWalkFrozen())
        {
            m_MovmentDirection = Vector2.zero;
        }

        if (doPushBack)
        {
            m_MovmentDirection = GetFacingDirection();
        }

        if (m_MovmentDirection != Vector2.zero)
        {
            m_MovmentDirection.Normalize();
        }

        if (doPushBack)
            m_Body.velocity = m_MovmentDirection * pushBackSpeed;
        else
            m_Body.velocity = m_MovmentDirection * m_speed;
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
