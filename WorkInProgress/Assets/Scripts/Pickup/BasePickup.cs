using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    protected Sprite sprite;
    protected PickupAnimation pickupAnimation;
    protected PlayerInventory m_inventory;
    protected float proportion;

    public enumInventory item;
    public GameObject playerSlime1;
    public GameObject playerSlime2;

    private InventoryUI inventoryUI;
    private RectTransform slotTransform;
    private PickupUseGeneralAnimation pickupUseGeneralAnimation;
    private RectTransform slotTrasform;

    protected void Awake()
    {
        pickupAnimation = PlayerInstant.Instance.GetComponentInChildren<PickupAnimation>();
    }

    private void Start()
    {
        gameObject.name = item.ToString();
        proportion = GetComponentInChildren<SpriteRenderer>().transform.localScale.x;
    }

    protected void ResetSelectedInventory()
    {
        (PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerInventory>()).ResetSlected();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
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

    protected void DoPickupAnimation()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        pickupAnimation.DoAnimation(sprite, proportion);
    }

    protected void DoCancelPickupAnimation()
    {
        GameObject Object = Resources.Load("CancelPickup") as GameObject;
        sprite = (Object.transform.GetComponentInChildren<SpriteRenderer>()).sprite;
        pickupAnimation.DoAnimation(sprite, 0.75f);
    }

    public void UsePickup()
    {
        PlayerStats playerStats = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerStats>();
        AttackablePlayer attackable = PlayerInstant.Instance.transform.gameObject.
            GetComponentInChildren<AttackablePlayer>();

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

        if (item == enumInventory.StunPickup)
        {
            createPlayerSlime();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        DoInventoryCancelAnimation();
    }

    private void createPlayerSlime()
    {

    }

    private void GetComponents()
    {
        inventoryUI = InventoryUI.Instance;
        pickupUseGeneralAnimation = inventoryUI.GetComponent<PickupUseGeneralAnimation>();
        slotTrasform = inventoryUI.GetSlotSelectedRectTransform();
    }

    protected void DoNonInstantiateAnimation()
    {
        GetComponents();

        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        pickupUseGeneralAnimation.DoAnimation(sprite, 1f, slotTrasform);
    }

    protected void DoInventoryCancelAnimation()
    {
        GetComponents();

        GameObject Object = Resources.Load("CancelPickup") as GameObject;
        sprite = (Object.transform.GetComponentInChildren<SpriteRenderer>()).sprite;
        pickupUseGeneralAnimation.DoAnimation(sprite, 0.75f, slotTrasform);
    }
}
