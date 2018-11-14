using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlime : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector2 m_facingDirection;

    public float damage = 1;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public float GetDamage()
    {
        return damage;
    }

    private void Start()
    {
        StartCoroutine(WaitBeforeFadeOut());
    }

    private IEnumerator WaitBeforeFadeOut()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SlimeFadeout());
    }

    private IEnumerator SlimeFadeout()
    {
        float opacity = 1f;

        while (opacity > 0.2f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject.transform.parent);
    }
}
