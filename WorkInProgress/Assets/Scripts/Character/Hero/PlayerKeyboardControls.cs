using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardControls : MonoBehaviour
{
    private CharacterBaseControl m_BaseControl;

    private void Awake()
    {
        m_BaseControl = GetComponent<CharacterBaseControl>();
    }

    private void Start()
    {
        m_BaseControl.SetDirection(new Vector2(0, -1));
    }

    private void Update()
    {
        if (PlayerAttributes.instance.IsGameStateFrozen() ||
            PlayerAttributes.instance.IsWalkFrozen())
        {
            return;
        }

        UpdateDirection();
        UpdateAction();
    }

    private void UpdateDirection()
    {
        Vector2 newDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_BaseControl.UpdateDirection(Directions.Up);
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            m_BaseControl.UpdateDirection(Directions.Down);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_BaseControl.UpdateDirection(Directions.Left);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_BaseControl.UpdateDirection(Directions.Right);
        }

        m_BaseControl.SetDirection(newDirection);
    }

    void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_BaseControl.OnActionPressed();
        }
    }
}
