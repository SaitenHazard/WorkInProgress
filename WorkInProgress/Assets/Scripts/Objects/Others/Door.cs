using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string warpScene;
    public string warpPoint;
    public Vector2 faceDirection;

    private SpriteRenderer spriteDoor;
    private CharacterMovementModel colliderMovementModel;

    private void Awake()
    {
        spriteDoor = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerStay2D (Collider2D collider)
    {
        if(collider.gameObject.name == "Player")
        {
            colliderMovementModel = collider.GetComponent<CharacterMovementModel>();
            Vector2 facingDirection = colliderMovementModel.GetFacingDirection();

            if (facingDirection == new Vector2(0, 1))
            {
                spriteDoor.enabled = true;
            }
        }
    }

    private void Warp()
    {
        colliderMovementModel.SetMovementFrozen(true);
        WarpManager.Instance.Warp(warpScene, warpPoint, faceDirection);
    }
}
