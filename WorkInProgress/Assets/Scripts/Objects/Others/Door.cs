using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    SpriteRenderer spriteDoor;

    private void Awake()
    {
        spriteDoor = GetComponentInChildren<SpriteRenderer>();
    }

    private void  OnCollidion(Collider2D collider)
    {
        if(collider.gameObject.name == "Player")
        {
            CharacterMovementModel colliderMovementModel = collider.GetComponent<CharacterMovementModel>();
            Vector2 facingDirection = colliderMovementModel.GetFacingDirection();

            if (facingDirection == new Vector2(0, 1))
                spriteDoor.enabled = true;
        }
    }
}
