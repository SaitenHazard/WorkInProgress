﻿using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

public class CharacterMovementModel : MonoBehaviour
{
    protected Vector2 m_MovementDirection;
    protected Vector2 m_FacingDirection;
    protected Vector2 m_ReceivedDirection;
    protected Rigidbody2D m_Body;
    protected PlayerStats playerStats;
    protected bool movementFrozen = false;

    private float recoilTime;
    private float m_pushBackSpeed;
    private float m_stunTime;

    public float speed;

    protected void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();

        recoilTime = 0.5f;
        m_stunTime = 0f;
    }

    protected void Update()
    {
        UpdateDirection();
        ResetReceivedDirection();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    public float GetRecoilTime()
    {
        return recoilTime;
    }

    private void ResetReceivedDirection()
    {
        m_ReceivedDirection = Vector2.zero;
    }

    public void SetTemporaryFrozen(float time)
    {
        StartCoroutine(TemporaryFrozenIenumerator(time));
    }

    private IEnumerator TemporaryFrozenIenumerator(float time)
    {
        SetMovementFrozen(true);
        yield return new WaitForSeconds(time);
        SetMovementFrozen(false);
    }

    public bool IsMovementFrozen()
    {
        return movementFrozen;
    }

    private void UpdateDirection()
    {
        if (movementFrozen || playerStats.GetGameState())
        {
            return;
        }

        m_MovementDirection = new Vector2(m_ReceivedDirection.x, m_ReceivedDirection.y);

        if (m_pushBackSpeed != 0f)
        {
            m_MovementDirection = m_FacingDirection;
            return;
        }

        if (m_ReceivedDirection != Vector2.zero)
        {
            Vector3 facingDirection = m_MovementDirection;

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

    private void UpdateMovement()
    {
        if (movementFrozen == true || playerStats.GetGameState() == true)
        {
            m_Body.velocity = Vector2.zero;
            return;
        }

        if (m_MovementDirection != Vector2.zero)
        {
            m_MovementDirection.Normalize();
        }

        float m_speed = speed;

        if (m_pushBackSpeed != 0f)
        {
            m_speed = m_pushBackSpeed;
        }

        m_Body.velocity = m_MovementDirection * m_speed;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }

        m_ReceivedDirection = direction;
    }

    public Vector3 GetDirection()
    {
        return m_MovementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return m_FacingDirection;
    }

    public bool IsMoving()
    {
        if (movementFrozen == true || playerStats.GetGameState())
        {
            return false;
        }

        return m_MovementDirection != Vector2.zero;
    }

    public void SetMovementFrozen(bool frozen)
    {
        movementFrozen = frozen;
    }

    public float GetPushBackSpeed()
    {
        return m_pushBackSpeed;
    }

    public bool IsInPushBack()
    {
        return m_pushBackSpeed != 0;
    }

    virtual public void DoAttack()
    {

    }

    public void DoPushBack(Vector2 pushBackDirection, float pushBackTime, float pushBackSpeed)
    {
        GetHit(pushBackDirection, pushBackTime, pushBackSpeed, 0f);
    }

    public void GetHit(Vector2 attackDirection, float pushBackTime, float pushBackSpeed, float stunTime)
    {
        m_FacingDirection = attackDirection;
        m_pushBackSpeed = pushBackSpeed;
        m_stunTime = stunTime;

        StartCoroutine(DoPushBack(pushBackTime));
    }

    private IEnumerator DoPushBack(float pushBackTime)
    {
        SetMovementFrozen(false);
        m_MovementDirection = m_FacingDirection;

        yield return new WaitForSeconds(pushBackTime);

        ReverseFacingDirection();
        m_pushBackSpeed = 0f;
        HitRecoil();
    }

    private void HitRecoil()
    {
        Attackable attackableEnemy = GetComponentInChildren<Attackable>();

        if (attackableEnemy == null)
            return;

        if (attackableEnemy.GetHealth() <= 0)
        {
            SetMovementFrozen(true);
            return;
        }

        SetTemporaryFrozen(recoilTime + m_stunTime);
        m_stunTime = 0f;
    }

    private void ReverseFacingDirection()
    {
        m_FacingDirection = GetReverseFacingDirection();
        UpdateDirection();
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float m_speed)
    {
        speed = m_speed;
    }

    public Vector2 GetReverseFacingDirection()
    {
        Vector2 direction = Vector2.zero;

        if (m_FacingDirection.x == 1)
            direction.x = -1;

        else if (m_FacingDirection.x == -1)
            direction.x = 1;

        else if (m_FacingDirection.y == 1)
            direction.y = -1;

        else if (m_FacingDirection.y == -1)
            direction.y = 1;

        return direction;
    }
}