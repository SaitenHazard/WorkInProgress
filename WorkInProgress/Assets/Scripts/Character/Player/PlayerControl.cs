﻿using UnityEngine;
using System.Collections;

public class PlayerControl : CharacterBaseControl
{
    private PlayerInventory m_inventory;
    public MenuView menuView;

    private void Awake()
    {
        base.Awake();
        m_inventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        SetDirection(new Vector2(0, -1));
    }

    private void Update()
    {
        UpdateMenuScreen();
        UpdateMenu();
        UpdateDirection();
        UpdateAction();
        UpdateInventory();
    }

    private void OnAttackPressed()
    {
        m_movementView.DoAttack();
        m_movementModel.DoAttack();
    }

    //KeybaordControls
    private void UpdateMenuScreen()
    {
        if (Input.GetKey(KeyCode.X))
        {
            if (menuView.GetMenuActive())
                menuView.SetMenuActive(true);
            else
                menuView.SetMenuActive(false);
        }
    }

    private void UpdateMenu()
    {
        if (menuView.GetMenuActive() == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                menuView.ChangeIndex(false);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                menuView.ChangeIndex(true);
            }

            return;
        }
    }

    private void UpdateInventory()
    {
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            m_inventory.DestorySelected();
        }
    }

    private void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnAttackPressed();
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