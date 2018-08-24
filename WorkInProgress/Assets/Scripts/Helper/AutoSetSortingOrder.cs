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
        if(transform.position.y < PlayerInstant.Instance.transform.position.y)
        {
            spriteRenderer.sortingOrder = 200;
        }
        else
        {
            spriteRenderer.sortingOrder = 0;
        }
    }
}
