using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    private Sprite sprite;
    public float proportion;

    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {

    }

    protected void DoPickupAnimation()
    {
        PickupAnimation pickupAnimation = PlayerInstant.Instance.transform.gameObject.GetComponentInChildren<PickupAnimation>();

        sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        pickupAnimation.DoAnimation(sprite, proportion);
        Destroy(gameObject);
    }

    virtual public void UsePickup()
    {

    }

    protected void DoNonInstantiateAnimation()
    {
        PickupUseGeneralAnimation pickupAnimation =  InventoryUI.Instance.transform.gameObject.GetComponentInChildren<PickupUseGeneralAnimation>();

        sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        RectTransform slotTrasform = InventoryUI.Instance.GetSlotSelectedRectTransform();

        pickupAnimation.DoAnimation(sprite, proportion, slotTrasform);
    }
}
