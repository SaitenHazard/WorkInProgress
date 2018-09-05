using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantPickupCoin : InstantPickupBase
{
    PlayerWallet playerWallet;

    private void Start()
    {
        playerWallet = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerWallet>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if(playerWallet.GetCoins() < 999)
            {
                playerWallet.AddCoin();
                DoPickupAnimation();
                Destroy(gameObject);

                return;
            }

            DoCancelPickupAnimation();
        }
    }
}
