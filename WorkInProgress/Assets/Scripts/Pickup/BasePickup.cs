using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickup : MonoBehaviour
{
    public PickupAnimation pickupAnimation;

    virtual protected void Awake()
    {
        pickupAnimation = PlayerInstant.Instance.gameObject.
            GetComponentInChildren<PickupAnimation>();

        Debug.Log(pickupAnimation);
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {
        
    }
}
