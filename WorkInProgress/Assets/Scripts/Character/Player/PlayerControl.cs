using UnityEngine;
using System.Collections;

public class PlayerControl : CharacterBaseControl
{
    private PlayerInventory m_inventory;
    private PlayerStats playerStats;

    public MenuView menuView;

    private void Awake()
    {
        base.Awake();
        m_inventory = GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (menuView.GetMenuActive() == false)
                menuView.SetMenuActive(true);
            else
                menuView.SetMenuActive(false);
        }
    }

    private void UpdateMenu()
    {
        if (menuView.GetMenuActive() == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                menuView.ChangeIndex(true);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                menuView.ChangeIndex(false);
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
            InteractableBase interactableBase = playerStats.GetInteractableBase();

            if (interactableBase != null)
                interactableBase.OnInteract();
            else
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