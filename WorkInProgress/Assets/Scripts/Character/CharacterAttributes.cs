using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

    protected bool m_WalkFrozen = false;

    public float Speed;
    public float bubbleSpeechHeight;

    private int health;
    private float pushBackTime;

    private bool hitState = false;

    public Vector2 attackDirection;
    public Vector2 facingDirection;

    public float GetPushBackTime()
    {
        return pushBackTime;
    }

    public void SetPushBackTime(float time)
    {
        pushBackTime = time;
    }

    public void SetHealth(int m_health)
    {
        health = m_health;
    }

    public void SetSpeed(float m_speed)
    {
        Speed = m_speed;
    }

    public void SetAttackDirection(Vector2 direction)
    {
        attackDirection = direction;
    }

    public Vector2 GetAttackDirection()
    {
        return attackDirection;
    }

    public void SetDirections(Vector2 direction)
    {
        facingDirection = direction;
    }

    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }

    public float GetSpeed()
    {
        return Speed;
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

    public void SetHitState(bool state)
    {
        hitState = state;
    }

    public bool IsHitState()
    {
        return hitState;
    }
}
