using UnityEngine;
using System.Collections;

public class PlayerControl : CharacterBaseControl
{
    PlayerInventory m_inventory;

    private void Start()
    {
        m_inventory = GetComponent<PlayerInventory>();
        SetDirection(new Vector2(0, -1));
    }

    private void Update()
    {
        UpdateDirection();
        UpdateAction();
    }

    private void OnAttackPressed()
    {
        m_movementView.DoAttack();
        m_movementModel.DoAttack();
    }

    //KeybaordControls
    private void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnAttackPressed();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_inventory.changeSelectedSlotID(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_inventory.changeSelectedSlotID(true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            m_inventory.UseSelected();
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