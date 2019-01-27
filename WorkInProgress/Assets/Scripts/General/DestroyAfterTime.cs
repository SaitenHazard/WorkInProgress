using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyAfter;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(ObjectFadeOut());
    }

    protected virtual IEnumerator ObjectFadeOut()
    {
        yield return new WaitForSeconds(destroyAfter);

        float opacity = 1f;

        while (opacity > 0.2f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject);
    }
}
