using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPickupHealth : InstantPickupBase
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Attackable attackable = collider.GetComponent<Attackable>();

            attackable.AddHealth(3);

            GeeneralPickup();
            Destroy(gameObject);
        }
    }
}
