﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

    public bool m_WalkFrozen = false;

    public float speed;
    public float bubbleSpeechHeight;
    public int health;

    protected bool attackState = false;
    protected bool hitState = false;

    protected void Awake()
    {

    }

    public float getSpeed()
    {
        return speed;
    }

    public float getBubbleSpeechHeight()
    {
        return bubbleSpeechHeight;
    }

    public Vector2 getCharacterPosition()
    {
        Vector2 positon = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        return positon;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }

    public bool IsWalkFrozen()
    {
        return m_WalkFrozen;
    }

    public void setWalkStateFrozen(bool walkState)
    {
        m_WalkFrozen = walkState;
    }

    public void SetAttackState(bool state)
    {
        attackState = state;
    }

    public bool IsAttackState()
    {
        return attackState;
    }

    public void SetHitState(bool state)
    {
        hitState = state;
    }

    public bool IsHitState()
    {
        return hitState;
    }
}