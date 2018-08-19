using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

    protected bool m_WalkFrozen = false;

    public float speed;
    public float bubbleSpeechHeight;
    public int health;
    public float pushBackTime;

    private bool hitState = false;

    private Vector2 attackDirection;
    private Vector2 facingDirection;

    public float GetPushBackTime()
    {
        return pushBackTime;
    }

    public void SetSpeed(float m_speed)
    {
        speed = m_speed;
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

    public void SetHitState(bool state)
    {
        hitState = state;
    }

    public bool IsHitState()
    {
        return hitState;
    }
}
