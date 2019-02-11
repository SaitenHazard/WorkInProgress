using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDestroyAnim : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnDestroy()
    {
        GameObject temp = Instantiate(Resources.Load("EnemyDestroy")) as GameObject;

        temp.transform.position = transform.position;

        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        Destroy(gameObject);
    }
}
