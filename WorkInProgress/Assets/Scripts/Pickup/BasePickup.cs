using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    public enumInventory item;

    private GameObject tempInstantiateObject;
    private InventoryUI inventoryUI;
    private RectTransform slotTransform;
    private PickupUseGeneralAnimation pickupUseGeneralAnimation;
    private RectTransform slotTrasform;
    private PlayerInstant playerInstance;
    private Sprite sprite;
    private PickupAnimation pickupAnimation;
    private PlayerInventory m_inventory;
    private float proportion;

    private void Start()
    {
        playerInstance = PlayerInstant.Instance;
        pickupAnimation = playerInstance.GetComponentInChildren<PickupAnimation>();

        gameObject.name = item.ToString();
        proportion = GetComponentInChildren<SpriteRenderer>().transform.localScale.x;
    }

    private void ResetSelectedInventory()
    {
        playerInstance = PlayerInstant.Instance;
        playerInstance.transform.gameObject.GetComponent<PlayerInventory>().ResetSlected();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            m_inventory = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerInventory>();

            if (m_inventory.GetInventorySize() < m_inventory.GetInventoryMaxSize())
            {
                m_inventory.AddItem(item);
                DoPickupAnimation();
                Destroy(gameObject);

                return;
            }

            DoCancelPickupAnimation();
        }
    }

    private void DoPickupAnimation()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        pickupAnimation.DoAnimation(sprite, proportion);
    }

    private void DoCancelPickupAnimation()
    {
        GameObject Object = Resources.Load("CancelPickup") as GameObject;
        sprite = (Object.transform.GetComponentInChildren<SpriteRenderer>()).sprite;
        pickupAnimation.DoAnimation(sprite, 0.75f);
    }

    public void UsePickup()
    {
        PlayerStats playerStats = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerStats>();
        AttackablePlayer attackable = PlayerInstant.Instance.transform.gameObject.GetComponentInChildren<AttackablePlayer>();

        if (item == enumInventory.ProjectilePickup && playerStats.IsProjetileActive() == false)
        {
            playerStats.SetProjectile();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.HealthPickup && attackable.GetHealth() < attackable.GetMaxHealth())
        {
            attackable.RestoreFullHealth();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.SpeedPickup && playerStats.IsSpeedUp() == false)
        {
            playerStats.SpeedUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.StrengthPickup && playerStats.IsDamageUp() == false)
        {
            playerStats.DamageUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.SlimePickup)
        {
            playerInstance = PlayerInstant.Instance;

            if (playerStats.IsDamageUp() == true)
                tempInstantiateObject = playerInstance.transform.Find("PlayerSlime2").gameObject;
            else
                tempInstantiateObject = playerInstance.transform.Find("PlayerSlime1").gameObject;

            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.RangePickup && playerStats.IsRangeUp() == false)
        {
            playerStats.RangeUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.InvisiblePickup && playerStats.IsInvisibleUp() == false)
        {
            playerStats.InvisibleUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.InvinciblePickup && playerStats.IsInvincibleUp() == false)
        {
            playerStats.InvincibleUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.DecoyPickup)
        {
            playerInstance = PlayerInstant.Instance;
            tempInstantiateObject = playerInstance.transform.Find("Decoy").gameObject;

            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            CreateInstantiatePikcup();
            return;
        }

        if (item == enumInventory.BombPickup)
        {
            playerInstance = PlayerInstant.Instance;
            tempInstantiateObject = playerInstance.transform.Find("Bomb").gameObject;

            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            CreateInstantiatePikcup();
            return;
        }

        DoInventoryCancelAnimation();
    }

    private void CreateInstantiatePikcup()
    {
        GameObject tempDecoy = Instantiate(tempInstantiateObject);

        tempDecoy.SetActive(true);

        CharacterMovementModel m_movementModel = playerInstance.GetComponent<CharacterMovementModel>();
        Vector2 facingDirection = m_movementModel.GetFacingDirection();

        if (facingDirection.x == 1)
        {
            tempDecoy.transform.position = new Vector3
                (playerInstance.transform.position.x + 0.5f, playerInstance.transform.position.y, 0);
        }
        else if (facingDirection.x == -1)
        {
            tempDecoy.transform.position = new Vector3
                (playerInstance.transform.position.x - 0.5f, playerInstance.transform.position.y, 0);
        }
        else if (facingDirection.y == 1)
        {
            tempDecoy.transform.position = new Vector3
                (playerInstance.transform.position.x, playerInstance.transform.position.y + 0.5f, 0);
        }
        else
        {
            tempDecoy.transform.position = new Vector3
                (playerInstance.transform.position.x, playerInstance.transform.position.y - 0.5f, 0);
        }
    }

    private void CreatePlayerSlime(GameObject playerSlime)
    {
        GameObject cloneObject = Instantiate(playerSlime);
        cloneObject.SetActive(true);
    }

    private void GetComponents()
    {
        inventoryUI = InventoryUI.Instance;
        pickupUseGeneralAnimation = inventoryUI.GetComponent<PickupUseGeneralAnimation>();
        slotTrasform = inventoryUI.GetSlotSelectedRectTransform();
    }

    private void DoNonInstantiateAnimation()
    {
        GetComponents();

        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        pickupUseGeneralAnimation.DoAnimation(sprite, 1f, slotTrasform);
    }

    private void DoInventoryCancelAnimation()
    {
        GetComponents();

        GameObject Object = Resources.Load("CancelPickup") as GameObject;
        sprite = (Object.transform.GetComponentInChildren<SpriteRenderer>()).sprite;
        pickupUseGeneralAnimation.DoAnimation(sprite, 0.75f, slotTrasform);
    }
}
