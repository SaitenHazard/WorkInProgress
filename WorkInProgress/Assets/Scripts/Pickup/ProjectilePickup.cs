using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePickup : BasePickup
{
    public override void UsePickup()
    {
        PlayerStats playerStats = PlayerInstant.Instance.transform.gameObject.GetComponent<PlayerStats>();

        if (playerStats.IsProjetileActive() == false)
        {
            playerStats.SetProjectile();
            DoNonInstantiateAnimation();
            ResetSelectedInventory();
            return;
        }

        DoInventoryCancelAnimation();
    }
}
