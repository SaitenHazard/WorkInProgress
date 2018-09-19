using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int coins = 0;

    public void AddCoin()
    {
        AddCoin(1);
    }

    public void AddCoin(int number)
    {
        coins += number;

        if (coins > 999)
            coins = 999;
    }

    public void SubstractCoin()
    {
        SubstractCoin(1);
    }

    public void SubstractCoin(int number)
    {
        coins -= number;
    }

    public void SetCoin(int l_coin)
    {
        coins = l_coin;
    }

    public int GetCoins()
    {
        return coins;
    }
}
