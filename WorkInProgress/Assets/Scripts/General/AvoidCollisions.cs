using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollisions : MonoBehaviour
{
    public string [] taglist;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < taglist.Length; i++)
            if (collision.collider.gameObject.tag == taglist[i])
            {
                Collider2D[] colliders = collision.collider.gameObject.GetComponents<Collider2D>();
                Physics2D.IgnoreCollision(colliders[0], GetComponent<Collider2D>());
            }
    }
}
