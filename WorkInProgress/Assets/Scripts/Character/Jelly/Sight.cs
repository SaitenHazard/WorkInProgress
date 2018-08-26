﻿using UnityEngine;
using System.Collections;

public class Sight : MonoBehaviour
{
    private CharacterMovementModel m_movementModel;
    private SpeechBubble speechBubble;
    private Vector2 movementDirection;
    private float angle;

    private void Awake()
    {
        speechBubble =
            transform.parent.GetComponentInChildren<SpeechBubble>();

        m_movementModel = GetComponentInParent<CharacterMovementModel>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            speechBubble.PopSpeechBubble(enumSpeechBubbles.Question);
        }
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            SetDirectionTowardsPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            SetNullDirection();
        }
    }

    private void Update()
    {
        m_movementModel.SetDirection(movementDirection);
    }

    private void SetNullDirection()
    {
        movementDirection = Vector2.zero;
    }

    private void SetDirectionTowardsPlayer()
    {
        float angle = 
            Mathf.Atan2(transform.position.y - 
            PlayerInstant.Instance.transform.position.y, 
            transform.position.x - PlayerInstant.Instance.transform.position.x) 
            * 180 / Mathf.PI *-1;

        if (angle >= 22.5 && angle <= 67.5)
            movementDirection = new Vector2(-1, 1);

        if (angle >= 112.5 && angle <= 157.5)
            movementDirection = new Vector2(1, 1);

        if (angle <= -22.5 && angle >= -67.5)
            movementDirection = new Vector2(-1, -1);

        if (angle <= -112.5 && angle >= -157.5)
            movementDirection = new Vector2(1, -1);

        if (angle >= 67.5 && angle <= 112.5)
            movementDirection = new Vector2(0, 1);

        if (angle <= -67.5 && angle >= -112.5)
            movementDirection = new Vector2(0, -1);

        if (angle <= 22.5 && angle >= 0  || angle >= -22.5 && angle <=0)
            movementDirection = new Vector2(-1, 0);

        if (angle >= 157.5 && angle <= 180 || angle <= -157.5 && angle >= -180)
            movementDirection = new Vector2(1, 0);
    }
}