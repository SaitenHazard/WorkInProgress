using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardControls : PlayerBaseControl
{
    private void Start()
    {
        SetDirection(new Vector2(0, -1));
    }

    private void Update()
    {
        UpdateDirection();
        UpdateAction();
    }

    private void UpdateDirection()
    {
        if (PlayerAttributes.instance.IsGameStateFrozen()) return;

        Vector2 newDirection = Vector2.zero;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            newDirection.y = 1;
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            newDirection.y = -1;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            newDirection.x = -1;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            newDirection.x = 1;
        }

        SetDirection(newDirection);
    }

    void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnActionPressed();
        }
    }
}
