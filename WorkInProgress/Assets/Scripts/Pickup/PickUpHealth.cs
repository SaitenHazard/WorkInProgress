using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : BaseCollider
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Attackable attackable =
                collider.transform.parent.GetComponent<Attackable>();

            attackable.AddHealth(3);

            Destroy(gameObject);
        }
    }
}
