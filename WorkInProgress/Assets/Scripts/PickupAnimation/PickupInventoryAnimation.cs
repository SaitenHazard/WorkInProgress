using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInventoryAnimation : MonoBehaviour {

    public IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        float opacity = 1f;
        float opacityIncrement = 0.2f;
        float yeildTime = 0.2f;

        while (opacity > 0f)
        {
            rigidbody.velocity = new Vector2(0, 1) * 0.5f;
            opacity -= opacityIncrement;
            m_spriteRenderer.color = new Color(1f, 1f, 1f, opacity);

            yield return new WaitForSeconds(yeildTime);
        }

        Destroy(gameObject);
    }
}
