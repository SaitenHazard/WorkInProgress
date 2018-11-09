using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetSortingOrder : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update ()
    {
        SetOrder();
	}

    private void SetOrder()
    {
        float floa = transform.position.y * -1;

        spriteRenderer.sortingOrder = (int)(transform.position.y * 100000f * -1f);
    }
}
