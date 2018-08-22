using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

public class CharacterMovementModel : MonoBehaviour
{
    public float Speed;

    protected Vector2 m_MovementDirection;
    protected Vector2 m_FacingDirection;
    protected bool movementFrozen;

    protected Rigidbody2D m_Body;

    protected Vector2 m_ReceivedDirection;
    protected CharacterAttributes m_Attributes;

    void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateDirection();
        ResetReceivedDirection();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void ResetReceivedDirection()
    {
        m_ReceivedDirection = Vector2.zero;
    }

    void UpdateDirection()
    {
        if (movementFrozen == true)
            return;

        m_MovementDirection = new Vector3(m_ReceivedDirection.x, m_ReceivedDirection.y, 0);

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

    void UpdateMovement()
    {
        if (m_MovementDirection != Vector2.zero)
        {
            m_MovementDirection.Normalize();
        }

        float speed = Speed;

        if (movementFrozen == true)
            speed = 0f;

        m_Body.velocity = m_MovementDirection * speed;
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
        return m_MovementDirection != Vector2.zero;
    }

    public void SetMovementFrozen(bool frozen)
    {
        movementFrozen = frozen;
    }

    virtual public void DoAttack()
    {

    }

    public void GetHit(Vector2 attackDirection, float pushBackTime)
    {
        m_FacingDirection = attackDirection;
        SetMovementFrozen(true);

        StartCoroutine(DoPushBack(pushBackTime));
    }

    private IEnumerator DoPushBack(float pushBackTime)
    {
        m_MovementDirection = m_FacingDirection;

        yield return new WaitForSeconds(pushBackTime);

        ReverseFacingDirection();

        SetMovementFrozen(false);
    }

    private Vector2 ReverseFacingDirection()
    {
        Vector2 newDirection = Vector2.zero;

        if (m_FacingDirection.x == 1)
            m_FacingDirection.x = -1;
        else if (m_FacingDirection.x == -1)
            m_FacingDirection.x = 1;
        else if (m_FacingDirection.y == 1)
            m_FacingDirection.y = -1;
        else
            m_FacingDirection.y = 1;

        return newDirection;
    }
}