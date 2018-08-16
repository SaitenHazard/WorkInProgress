using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

    public bool m_freezeGameState = false;
    public bool m_WalkFrozen = false;

    public float speed;
    public float bubbleSpeechHeight;

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

    public bool IsWalkFrozen()
    {
        return m_WalkFrozen;
    }

    public bool IsGameStateFrozen()
    {
        return m_freezeGameState;
    }

    public void setWalkStateFrozen(bool walkState)
    {
        m_WalkFrozen = walkState;
    }

    public void setFreezeGameState(bool freezeGameState)
    {
        m_freezeGameState = freezeGameState;
    }
}
