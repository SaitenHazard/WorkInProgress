using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInteractionCollision : MonoBehaviour
{
    private CharacterMovementModel m_movementModel;
    private Collider2D m_collider2D;

    private void Awake()
    {
        m_collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        m_movementModel = PlayerInstant.Instance.GetComponent<CharacterMovementModel>();
        Vector2 facingDirection = m_movementModel.GetFacingDirection();

        if(facingDirection.x == 1)
        {
            m_collider2D.offset = new Vector2(0.4f, 0f);
        }

        else if (facingDirection.x == -1)
        {
            m_collider2D.offset = new Vector2(-0.4f, 0f);
        }

        else if (facingDirection.y == -1)
        {
            m_collider2D.offset = new Vector2(0f, -0.4f);
        }

        else if (facingDirection.y == 1)
        {
            m_collider2D.offset = new Vector2(0f, 0.4f);
        }
    }
}
