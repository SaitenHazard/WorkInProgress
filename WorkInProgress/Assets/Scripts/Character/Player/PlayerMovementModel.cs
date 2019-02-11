using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModel : CharacterMovementModel
{
    private PlayerProjectile playerProjectile;

    private void Awake()
    {
        playerProjectile = GetComponent<PlayerProjectile>();
        base.Awake();
    }

    private void Update()
    {
        speed = playerStats.GetSpeed();
        base.Update();
    }

    override public void DoAttack()
    {
        if (GetPushBackSpeed() != 0)
            return;

        SetMovementFrozen(true);

        if (playerStats.IsProjetileActive() == true)
        {
            playerProjectile.DoPorjectile();
        }
    }
}
