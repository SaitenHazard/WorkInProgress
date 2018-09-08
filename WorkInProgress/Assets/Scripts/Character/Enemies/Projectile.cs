using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 direction;
    private float speed;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        direction = PlayerInstant.Instance.transform.position - transform.position;
        direction = direction.normalized;
    }

    private void Update()
    {
        rigidbody2D.velocity = direction * 1.5f;
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
