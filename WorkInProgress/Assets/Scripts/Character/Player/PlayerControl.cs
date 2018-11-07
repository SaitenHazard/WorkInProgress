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
        UpdateMenus();
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

    private void UpdateMenus()
    {
        if (TitleScreenView.Instance != null)
        {
            if (TitleScreenView.Instance.GetTitleScreenActive() == true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    TitleScreenView.Instance.ChangeIndex(true, false, false, false);

                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    TitleScreenView.Instance.ChangeIndex(false, true, false, false);

                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    TitleScreenView.Instance.ChangeIndex(false, false, true, false);

                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    TitleScreenView.Instance.ChangeIndex(false, false, false, true);

                if (Input.GetKeyDown(KeyCode.D))
                {
                    TitleScreenView.Instance.ActionPresed();
                }
            }

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
            }
        }
    }

    private void UpdateInventory()
    {
        if (menuView.GetMenuActive() == true)
            return;

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
            if (menuView.GetMenuActive() == true)
                TitleScreenView.Instance.ActionPresed();

            InteractableBase interactableBase = playerStats.GetInteractableBase();

            if(SpeechTextUI.Instance != null)
            {
                if (SpeechTextUI.Instance.GetTextBoxActive() == true)
                {
                    interactableBase.GetComponent<SpeechBase>().DoSpeech();

                    return;
                }
            }

            if (interactableBase != null)
                interactableBase.OnInteract();
            else
                OnAttackPressed();
        }
    }

    private void UpdateDirection()
    {
        if (menuView.GetMenuActive() == true)
            return;

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