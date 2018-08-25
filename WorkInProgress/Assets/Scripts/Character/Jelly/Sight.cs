using UnityEngine;
using System.Collections;

public class Sight : MonoBehaviour
{
    private CharacterMovementModel m_movementModel;
    private SpeechBubble speechBubble;
    private GameObject player;
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
        if (collider2D.gameObject.name == "Player")
        {
            speechBubble.PopSpeechBubble(enumSpeechBubbles.Question);
            player = collider2D.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name == "Player")
        {
            SetDirectionTowardsPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name == "Player")
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
        float angle = Mathf.Atan2(transform.position.y - PlayerInstant.Instance.transform.position.y, transform.position.x - PlayerInstant.Instance.transform.position.x) * 180 / Mathf.PI;

        //bottom-right
        if (angle >= 22.5 && angle <= 67.5)
            movementDirection = new Vector2(1, -1);

        //bottom-left
        if (angle >= 112.5 && angle <= 157.5)
            movementDirection = new Vector2(-1, -1);

        //top-right
        if (angle <= -22.5 && angle >= -67.5)
            movementDirection = new Vector2(1, 1);

        //top-left
        if (angle <= -112.5 && angle >= -157.5)
            movementDirection = new Vector2(-1, 1);

        //bottom
        if (angle >= 67.5 && angle <= 112.5)
            movementDirection = new Vector2(0, -1);

        //top
        if (angle <= -67.5 && angle >= -112.5)
            movementDirection = new Vector2(0, 1);

        //left
        if (angle <= 22.5 && angle >= -22.5)
            movementDirection = new Vector2(-1, 0);

        //right
        if (angle >= 157.5 && angle <= 180 || angle <= -157.5 && angle >= -180)
            movementDirection = new Vector2(1, 0);
    }
}