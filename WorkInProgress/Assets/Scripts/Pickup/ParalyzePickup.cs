using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalyzePickup : BasePickup
{
    public override void UsePickup()
    {
        PlayerStats playerStats = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerStats>();

        if (playerStats.IsDamageUp() == false)
        {
            playerStats.SetParalyzeUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        DoInventoryCancelAnimation();
    }
}
