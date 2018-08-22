using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : CharacterAttributes {

    private bool attackState = false;
    private bool m_freezeGameState = false;
    private float pushBackSpeed = 2.0f;

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
        attackState = state;
    }

    public bool IsAttackState()
    {
        return attackState;
    }
}
