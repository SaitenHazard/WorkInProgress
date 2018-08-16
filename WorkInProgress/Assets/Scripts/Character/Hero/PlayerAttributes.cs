using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : CharacterAttributes {

    public static PlayerAttributes instance;
    public bool attackState = false;

    protected void Awake()
    {
        base.Awake();

        instance = this;
    }

    public void SetAttackState(bool state)
    {
        attackState = state;
    }

    public bool IsAttackState()
    {
        return attackState;
    }
}
