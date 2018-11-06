using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : DestroyAfterTime
{
    public float damage;

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

    public Vector2 GetHitDirection(Transform enemyTransform)
    {
        Vector2 hitDirection = Vector2.zero;

        float angle = Mathf.Atan2 (enemyTransform.transform.position.y - gameObject.transform.position.y,
                enemyTransform.transform.position.x - gameObject.transform.position.y)
                * 180 / Mathf.PI * -1;

        Debug.Log(angle);

        if (angle >= 45 && angle <= 135)
        {
            hitDirection = new Vector2(0, 1);
        }

        if (angle <= -45 && angle >= -135)
        {
            hitDirection = new Vector2(0, -1);
        }

        if (angle >= 0 && angle < 45 || angle < 0 && angle > -45)
        {
            hitDirection = new Vector2(-1, 0);
        }

        if (angle > 135 && angle <= 180 || angle < -135 && angle >= -180)
        {
            hitDirection = new Vector2(1, 0);
        }

        return hitDirection;
    }
}
