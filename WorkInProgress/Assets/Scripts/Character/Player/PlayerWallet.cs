using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int coins = 0;

    public void AddCoin()
    {
        AddCoin(1);
    }

    public void AddCoin(int number)
    {
        coins += number;
    }

    public void SubstractCoin()
    {
        SubstractCoin(1);
    }

    public void SubstractCoin(int number)
    {
        coins -= number;
    }

    public int GetCoins()
    {
        return coins;
    }
}
