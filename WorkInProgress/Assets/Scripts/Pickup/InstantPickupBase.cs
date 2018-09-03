using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPickupBase : MonoBehaviour {

    private Sprite sprite;
    public float proportion;

    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {

    }

    protected void GeeneralPickup()
    {
        PickupAnimation pickupAnimation = PlayerInstant.Instance.transform.
            gameObject.GetComponentInChildren<PickupAnimation>();

        sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        pickupAnimation.DoAnimation(sprite, 0.5f);
        Destroy(gameObject);
    }
}
