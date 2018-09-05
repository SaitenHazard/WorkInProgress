using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : BasePickup
{
    public override void UsePickup()
    {
        AttackablePlayer attackable = PlayerInstant.Instance.transform.gameObject.
            GetComponentInChildren<AttackablePlayer>();

        if (attackable.GetHealth() < attackable.GetMaxHealth())
        {
            attackable.RestoreFullHealth();
            ResetSelectedInventory();
        }

        DoInventoryCancelAnimation();
    }
}
