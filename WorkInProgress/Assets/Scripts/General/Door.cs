﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string warpScene;
    public string warpPoint;
    public Vector2 faceDirection;

    private bool warping;

    private SpriteRenderer spriteDoor;
    private CharacterMovementModel colliderMovementModel;

    private void Awake()
    {
        spriteDoor = GetComponentInChildren<SpriteRenderer>();
        warping = false;
    }

    private void OnTriggerStay2D (Collider2D collider)
    {
        if (warping) return;

        if(collider.gameObject.tag == "Player")
        {
            colliderMovementModel = collider.GetComponentInParent<CharacterMovementModel>();
            Vector2 facingDirection = colliderMovementModel.GetFacingDirection();

            if (facingDirection == new Vector2(0, 1))
            {
                warping = true;
                spriteDoor.enabled = true;
                StartCoroutine(Warp());
            }
        }
    }

    private IEnumerator Warp()
    {
        colliderMovementModel.SetMovementFrozen(true);

        yield return StartCoroutine(Fade.Instance.FadeOut());

        WarpManager.Instance.Warp(warpScene, warpPoint, faceDirection);
    }
}
