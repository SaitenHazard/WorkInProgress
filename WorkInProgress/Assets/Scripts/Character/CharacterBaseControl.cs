using UnityEngine;
using System.Collections;

public class CharacterBaseControl : MonoBehaviour
{
    protected CharacterMovementModel m_movementModel;
    protected CharacterMovementView m_movementView;

    void Awake()
    {
        m_movementModel = GetComponent<CharacterMovementModel>();
        m_movementView = GetComponent<CharacterMovementView>();
    }

    protected Vector2 GetDiagonalizedDirection(Vector2 direction, float threshold)
    {
        if (Mathf.Abs(direction.x) < threshold)
        {
            direction.x = 0;
        }
        else
        {
            direction.x = Mathf.Sign(direction.x);
        }

        if (Mathf.Abs(direction.y) < threshold)
        {
            direction.y = 0;
        }
        else
        {
            direction.y = Mathf.Sign(direction.y);
        }

        return direction;
    }

    protected void SetDirection(Vector2 direction)
    {
        if (m_movementModel == null)
        {
            return;
        }

        m_movementModel.SetDirection(direction);
    }

    protected void DoHit(Vector2 hitDirection)
    {

    }
}