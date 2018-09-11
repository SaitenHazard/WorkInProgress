using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetSortingOrder : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Transform m_transform;
    private Transform p_Trasnform;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_transform = transform.parent.Find("MidPoint").transform;
        p_Trasnform = PlayerInstant.Instance.GetComponent<Transform>();
    }

    private void Update ()
    {
        SetOrder();
	}

    private void SetOrder()
    {
        Vector3 m_position = m_transform.TransformPoint(Vector3.zero);
        Vector3 p_position = p_Trasnform.TransformPoint(Vector3.zero);

        if (m_position.y < p_position.y)
        {
            spriteRenderer.sortingOrder = 200;
        }
        else
        {
            spriteRenderer.sortingOrder = 0;
        }
    }
}
