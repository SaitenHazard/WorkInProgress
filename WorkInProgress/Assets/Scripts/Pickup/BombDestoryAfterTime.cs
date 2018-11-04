using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDestoryAfterTime : DestroyAfterTime
{
    protected override IEnumerator ObjectFadeOut()
    {
        yield return new WaitForSeconds(destoryAfter);

        float opacity = 1f;

        while (opacity > 0.2f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        GetComponentInChildren<CircleCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
