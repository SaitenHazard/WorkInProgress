﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPickupCoin : InstantPickupBase
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerWallet playerWallet =
                collider.GetComponentInParent<PlayerWallet>();

            playerWallet.AddCoin();

            DoPickupAnimation();
            Destroy(gameObject);
        }
    }
}
