using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    public enumInventory item;

    private GameObject playerSlime1;
    private GameObject playerSlime2;
    private InventoryUI inventoryUI;
    private RectTransform slotTransform;
    private PickupUseGeneralAnimation pickupUseGeneralAnimation;
    private RectTransform slotTrasform;
    private PlayerInstant playerInstance;
    private Sprite sprite;
    private PickupAnimation pickupAnimation;
    private PlayerInventory m_inventory;
    private float proportion;

    protected void Awake()
    {
        pickupAnimation = PlayerInstant.Instance.GetComponentInChildren<PickupAnimation>();
        playerInstance = PlayerInstant.Instance;
    }

    private void Start()
    {
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

        if (item == enumInventory.StunPickup && playerStats.IsStunUp() == false)
        {
            playerStats.StunUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        if (item == enumInventory.SlimePickup)
        {
            playerInstance = PlayerInstant.Instance;
            playerSlime1 = playerInstance.transform.Find("PlayerSlime1").gameObject;
            playerSlime2 = playerInstance.transform.Find("PlayerSlime2").gameObject;

            Debug.Log(playerSlime1);
            Debug.Log(playerSlime2);

            if (playerStats.IsDamageUp() == true)
                createPlayerSlime(playerSlime2);
            else
                createPlayerSlime(playerSlime1);

            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        DoInventoryCancelAnimation();
    }

    private void createPlayerSlime(GameObject playerSlime)
    {
        Debug.Log(playerSlime);

        GameObject cloneObject = Instantiate(playerSlime, playerInstance.transform);
        cloneObject.transform.SetParent(null);
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
