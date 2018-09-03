using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    protected PickupAnimation pickupAnimation;

    virtual protected void Awake()
    {
        pickupAnimation = PlayerInstant.Instance.transform.
            gameObject.GetComponentInChildren<PickupAnimation>();
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {
        
    }
}
