using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : BasePickup
{
    private PlayerInventory m_inventory;
    public enumInventory item;

    private void Awake()
    {
        m_inventory = 
            (Resources.Load("Player") as GameObject).GetComponent<PlayerInventory>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(m_inventory.GetInventorySize());
        Debug.Log(m_inventory.GetInventoryMaxSize());

        if (collider.tag == "Player" && m_inventory.GetInventorySize() < 
            m_inventory.GetInventoryMaxSize())
        {
            m_inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
