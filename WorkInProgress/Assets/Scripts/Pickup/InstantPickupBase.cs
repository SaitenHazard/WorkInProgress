using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPickupBase : MonoBehaviour {

    private Sprite sprite;
    private PickupAnimation pickupAnimation;

    public float proportion;

    private void Awake()
    {
        pickupAnimation = PlayerInstant.Instance.gameObject.GetComponentInChildren<PickupAnimation>();
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {

    }

    protected void DoPickupAnimation()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        pickupAnimation.DoAnimation(sprite, proportion);
        Destroy(gameObject);
    }

    protected void DoCancelPickupAnimation()
    {
        GameObject Object = Resources.Load("CancelPickup") as GameObject;

        sprite = (Object.transform.GetComponentInChildren<SpriteRenderer>()).sprite;

        pickupAnimation.DoAnimation(sprite, 0.75f);
    }
}
