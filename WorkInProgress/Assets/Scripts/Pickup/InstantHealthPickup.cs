using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantHealthPickup : BasePickup
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Attackable attackable =
                collider.GetComponent<Attackable>();

            Debug.Log(attackable);

            attackable.AddHealth(3);

            Destroy(gameObject);
        }
    }
}
