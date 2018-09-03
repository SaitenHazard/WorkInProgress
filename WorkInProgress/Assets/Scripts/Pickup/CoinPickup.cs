﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : BasePickup
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerWallet playerWallet =
                collider.GetComponentInParent<PlayerWallet>();

            playerWallet.AddCoin();

            Sprite sprite = GetComponentInChildren<SpriteRenderer>().sprite;

            pickupAnimation.SetPickupAnimation(sprite, 0.5f);

            Destroy(gameObject);
        }
    }
}
