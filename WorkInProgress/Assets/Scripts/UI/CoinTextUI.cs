using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextUI : MonoBehaviour
{
    PlayerWallet playerWallet;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        playerWallet = PlayerInstant.Instance.GetComponent<PlayerWallet>();
    }

    private void Update()
    {
        text.text = playerWallet.GetCoins().ToString();
    }
}
