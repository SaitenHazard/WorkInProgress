using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    private Vector2 direction;
    private float speed;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = 2;
    }

    public void SetDirectionTowardsPlayerFacing()
    {
        direction = PlayerInstant.Instance.GetComponent<CharacterMovementModel>().GetFacingDirection();
    }

    public void SetDirectionTowardsPlayer()
    {
        direction = PlayerInstant.Instance.transform.position - transform.position;
        direction = direction.normalized;
    }

    public Vector2 GetMovementDirection()
    {
        return direction;
    }

    public Vector2 GetReverseDirection()
    {
        if (direction.x == 1)
            direction.x = -1;

        else if (direction.x == -1)
            direction.x = 1;

        else if (direction.y == 1)
            direction.y = -1;

        else if (direction.y == -1)
            direction.y = 1;

        return direction;
    }

    public Vector2 GetHitDirection()
    {
        float angle;
        direction = Vector2.zero;

        angle = Mathf.Atan2(transform.position.y - PlayerInstant.Instance.transform.position.y,
            transform.position.x - PlayerInstant.Instance.transform.position.x) * 180 / Mathf.PI * -1;

        if (angle >= 22.5 && angle <= 67.5)
            direction = new Vector2(-1, 1);

        if (angle >= 112.5 && angle <= 157.5)
            direction = new Vector2(1, 1);

        if (angle <= -22.5 && angle >= -67.5)
            direction = new Vector2(-1, -1);

        if (angle <= -112.5 && angle >= -157.5)
            direction = new Vector2(1, -1);

        if (angle >= 67.5 && angle <= 112.5)
            direction = new Vector2(0, 1);

        if (angle <= -67.5 && angle >= -112.5)
            direction = new Vector2(0, -1);

        if (angle <= 22.5 && angle >= 0 || angle >= -22.5 && angle <= 0)
            direction = new Vector2(-1, 0);

        if (angle >= 157.5 && angle <= 180 || angle <= -157.5 && angle >= -180)
            direction = new Vector2(1, 0);

        return direction;
    }

    private void Update()
    {
        m_rigidbody2D.velocity = direction * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void DestroyOnHit()
    {
        Destroy(gameObject);
    }
}
