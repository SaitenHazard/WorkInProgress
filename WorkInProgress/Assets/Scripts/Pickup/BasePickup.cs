using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    protected Sprite sprite;
    protected PickupAnimation pickupAnimation;
    protected PlayerInventory m_inventory;
    private float proportion;

    public enumInventory item;

    private void Awake()
    {
        m_inventory = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerInventory>();
        pickupAnimation = PlayerInstant.Instance.transform.gameObject.GetComponentInChildren<PickupAnimation>();
    }

    private void Start()
    {
        gameObject.name = item.ToString();
        proportion = GetComponentInChildren<SpriteRenderer>().transform.localScale.x;
    }

    protected void ResetSelectedInventory()
    {
        DoNonInstantiateAnimation();
        (PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerInventory>()).ResetSlected();
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
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

        pickupAnimation.DoAnimation(sprite, 0.5f);
    }

    virtual public void UsePickup()
    {

    }

    protected void DoNonInstantiateAnimation()
    {
        PickupUseGeneralAnimation pickupUseGeneralAnimation = InventoryUI.Instance.transform.gameObject.GetComponent<PickupUseGeneralAnimation>();
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        RectTransform slotTrasform = InventoryUI.Instance.GetSlotSelectedRectTransform();
        pickupUseGeneralAnimation.DoAnimation(sprite, proportion, slotTrasform);
    }

    protected void DoInventoryCancelAnimation()
    {
        PickupUseGeneralAnimation pickupUseGeneralAnimation = InventoryUI.Instance.transform.gameObject.GetComponent<PickupUseGeneralAnimation>();
        GameObject Object = Resources.Load("CancelPickup") as GameObject;
        sprite = (Object.transform.GetComponentInChildren<SpriteRenderer>()).sprite;
        RectTransform slotTrasform = InventoryUI.Instance.GetSlotSelectedRectTransform();
        pickupUseGeneralAnimation.DoAnimation(sprite, 0.75f, slotTrasform);
    }
}
