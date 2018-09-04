using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : BasePickup
{
    private PlayerInventory m_inventory;
    public enumInventory item;

    private void Start()
    {
        m_inventory = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerInventory>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && m_inventory.GetInventorySize() <
            m_inventory.GetInventoryMaxSize())
        {
            m_inventory.AddItem(item);

            DoPickupAnimation();
            Destroy(gameObject);
        }
    }

    public override void UsePickup()
    {
        AttackablePlayer attackable = PlayerInstant.Instance.transform.gameObject.
            GetComponentInChildren<AttackablePlayer>();

        attackable.RestoreFullHealth();
        DoNonInstantiateAnimation();
    }
}
