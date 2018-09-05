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

            if (attackable.GetHealth() < attackable.GetMaxHealth())
            {
                attackable.AddHealth(3);

                DoPickupAnimation();
                Destroy(gameObject);
            }

            DoCancelPickupAnimation();
        }
    }
}
