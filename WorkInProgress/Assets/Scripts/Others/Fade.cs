using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DoFade(bool fadeIn)
    {
        if (fadeIn)
            StartCoroutine(FadeIn());
        else
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color color = spriteRenderer.color;
        float opacity = 1f;

        while (color.a > 0f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            color.a = opacity;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator FadeIn()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color color = spriteRenderer.color;
        float opacity = 0f;

        while (color.a < 0f)
        {
            opacity += 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            color.a = opacity;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
