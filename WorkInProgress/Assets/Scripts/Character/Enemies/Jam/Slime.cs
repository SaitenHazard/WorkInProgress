using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(characterFadeOut());
    }

    private IEnumerator characterFadeOut()
    {
        yield return new WaitForSeconds(2.5f);

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color color = spriteRenderer.color;

        float opacity = 1f;

        while (opacity > 0f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(gameObject);
    }
}
