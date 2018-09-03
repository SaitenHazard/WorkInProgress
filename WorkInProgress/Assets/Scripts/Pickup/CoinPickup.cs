using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : BasePickup
{
    private Sprite sprite;
    public float proportion;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerWallet playerWallet =
                collider.GetComponentInParent<PlayerWallet>();

            playerWallet.AddCoin();

            GeeneralPickup();
        }
    }

    private void GeeneralPickup()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        pickupAnimation.DoAnimation(sprite, 0.5f);
        Destroy(gameObject);
    }
}
