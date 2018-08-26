using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade Instance;

    private SpriteRenderer spriteRenderer;
    private Color color;
    private float opacityIncrement;
    private float yeildTime;

    private void Awake()
    {
        Instance = this;

        opacityIncrement = 0.2f;
        yeildTime = 0.2f;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    public float GetYeildTime()
    {
        return yeildTime;
    }

    public void DoFade(bool fadeIn)
    {
        if (fadeIn == true)
            StartCoroutine(FadeIn());
        else
            StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("in1");
        float opacity = 1f;

        while (color.a > 0f)
        {
            opacity -= opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            color.a = opacity;
            yield return new WaitForSeconds(yeildTime);
        }
    }

    private IEnumerator FadeOut()
    {
        Debug.Log("in2");
        float opacity = 0f;

        while (color.a < 1f)
        {
            opacity += opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            color.a = opacity;
            yield return new WaitForSeconds(yeildTime);
        }
    }
}
