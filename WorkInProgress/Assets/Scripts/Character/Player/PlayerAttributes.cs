using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : CharacterAttributes {

    private bool attackState = false;
    private bool m_freezeGameState = false;

    public static PlayerAttributes instance;

    private void Awake()
    {
        instance = this;
    }

    public bool IsGameStateFrozen()
    {
        return m_freezeGameState;
    }

    public void setFreezeGameState(bool freezeGameState)
    {
        m_freezeGameState = freezeGameState;
    }

    public void SetAttackState(bool state)
    {
        if (attackState == false)
            setWalkStateFrozen(true);

        attackState = state;
    }

    public bool IsAttackState()
    {
        return attackState;
    }
}
