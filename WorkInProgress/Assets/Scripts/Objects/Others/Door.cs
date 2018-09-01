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

private void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            colliderMovementModel = collider.GetComponentInParent<CharacterMovementModel>();
            Vector2 facingDirection = colliderMovementModel.GetFacingDirection();

            if (facingDirection == new Vector2(0, 1))
            {
                spriteDoor.enabled = true;
                StartCoroutine(Warp());
            }
        }
    }

    private IEnumerator Warp()
    {
        colliderMovementModel.SetMovementFrozen(true);

        Debug.Log("1");
        yield return StartCoroutine(Fade.Instance.FadeOut());

        Debug.Log("2");
        WarpManager.Instance.Warp(warpScene, warpPoint, faceDirection);

        Debug.Log("3");
        yield return StartCoroutine(Fade.Instance.FadeIn());
    }
}
