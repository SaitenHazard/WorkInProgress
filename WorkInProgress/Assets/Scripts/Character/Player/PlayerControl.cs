using UnityEngine;
using System.Collections;

public class PlayerControl : CharacterBaseControl
{
    private void Start()
    {
        SetDirection(new Vector2(0, -1));
    }

    private void Update()
    {
        UpdateDirection();
        UpdateAttack();
    }

    private void OnAttackPressed()
    {
        m_movementView.DoAttack();
        m_movementModel.DoAttack();
    }

    //KeybaordControls
    private void UpdateAttack()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnAttackPressed();
        }
    }

    private void UpdateActions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //OnActionPressed();
        }
    }

    private void UpdateDirection()
    {
        Vector2 newDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newDirection.y = 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            newDirection.y = -1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newDirection.x = -1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            newDirection.x = 1;
        }

        SetDirection(newDirection);
    }
}