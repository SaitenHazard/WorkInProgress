using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : BasePickup {

    public override void UsePickup()
    { 
        PlayerStats playerStats = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerStats>();

        if(playerStats.IsSpeedUp() == false)
        {
            playerStats.SpeedUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        DoInventoryCancelAnimation();
    }
}
