﻿using System.Collections;
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
        base.Update();
    }

    override public void DoAttack()
    {
        if (IsInPushBack() == true)
            return;

        SetMovementFrozen(true);

        if (playerStats.IsProjetileActive() == true)
        {
            playerProjectile.DoPorjectile();
        }
    }
}
