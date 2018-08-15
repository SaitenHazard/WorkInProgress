using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour {

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
}
