using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhambaSight : AIBase
{
    virtual protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerEnter2D(collider2D);
            StartCoroutine(ProjectileInstantiate());
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerExit2D(collider2D);
            StopAllCoroutines();
        }
    }
}
