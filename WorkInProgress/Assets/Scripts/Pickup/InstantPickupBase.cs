using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPickupBase : MonoBehaviour {

    private Sprite sprite;
    private PickupAnimation pickupAnimation;

    public float proportion;

    private void Awake()
    {
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {

    }

    protected void DoPickupAnimation()
    {
        pickupAnimation = PlayerInstant.Instance.gameObject.GetComponentInChildren<PickupAnimation>();

        sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        pickupAnimation.DoAnimation(sprite, proportion);
        Destroy(gameObject);
    }

    protected void DoCancelPickupAnimation()
    {
        pickupAnimation = PlayerInstant.Instance.gameObject.GetComponentInChildren<PickupAnimation>();

        GameObject Object = Resources.Load("CancelPickup") as GameObject;

        sprite = (Object.transform.GetComponentInChildren<SpriteRenderer>()).sprite;

        pickupAnimation.DoAnimation(sprite, 0.5f);
    }
}
