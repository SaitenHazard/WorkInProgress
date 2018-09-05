using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPickup : BasePickup {

    public override void UsePickup()
    { 
        PlayerStats playerStats = PlayerInstant.Instance.transform.gameObject.
            GetComponent<PlayerStats>();

        if(playerStats.IsDamageUp() == true)
        {
            playerStats.DamageUp();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        DoInventoryCancelAnimation();
    }
}
