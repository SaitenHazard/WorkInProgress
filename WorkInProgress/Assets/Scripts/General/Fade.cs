using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade Instance;

    protected SpriteRenderer spriteRenderer;
    protected Color color;
    protected float opacityIncrement;
    protected float yeildTime;

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
        if (fadeIn == false)
            StartCoroutine(FadeIn());
        else
            StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        float opacity = 1f;

        while (opacity > 0f)
        {
            opacity -= opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(yeildTime);
        }
    }

    public IEnumerator FadeInAndOut()
    {
        float opacity = 0f;

        while (opacity < 1f)
        {
            opacity += opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(yeildTime);
        }

        while (opacity > 0f)
        {
            opacity -= opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(yeildTime);
        }
    }

    public IEnumerator FadeOut()
    {
        float opacity = 0f;

        while (opacity < 1f)
        {
            opacity += opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(yeildTime);
        }
    }
}
