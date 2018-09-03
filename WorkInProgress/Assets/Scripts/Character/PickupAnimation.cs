﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    private GameObject cloneObject;
    private SpriteRenderer spriteRenderer;

    public void DoAnimation(Sprite sprite, float proportion)
    {
        SetPickupAnimation(sprite, proportion);
        StartAnimation();
    }

    private void StartAnimation()
    {
        PickupAnimation clonePickupAnimation = cloneObject.GetComponent<PickupAnimation>();
        StartCoroutine(clonePickupAnimation.Animate());
    }

    private void SetPickupAnimation(Sprite sprite, float proportion)
    {
        cloneObject = Instantiate(gameObject,
            transform.position,
            transform.rotation );

        spriteRenderer = 
            cloneObject.GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = sprite;
        cloneObject.transform.localScale = new Vector3(proportion, proportion, 0f);
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigitbody = GetComponent<Rigidbody2D>();

        float opacity = 1f;
        float opacityIncrement = 0.2f;
        float yeildTime = 0.2f;

        while (opacity > 0f)
        {
            rigitbody.velocity = transform.up * 1;
            opacity -= opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);

            yield return new WaitForSeconds(yeildTime);
        }

        Destroy(gameObject);
    }
}
