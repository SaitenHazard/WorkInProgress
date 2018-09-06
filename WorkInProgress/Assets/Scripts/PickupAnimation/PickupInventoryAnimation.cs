using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupInventoryAnimation : MonoBehaviour
{
    public IEnumerator UseAnimate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Image image = GetComponent<Image>();

        float opacity = 1f;
        float opacityIncrement = 0.2f;
        float yeildTime = 0.2f;

        while (opacity > 0f)
        {
            rigidbody.velocity = new Vector2(0, 1) * 0.5f;
            opacity -= opacityIncrement;
            image.color = new Color(1f, 1f, 1f, opacity);

            yield return new WaitForSeconds(yeildTime);
        }

        Destroy(gameObject);
    }

    public IEnumerator DestroyAnimate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Image image = GetComponent<Image>();

        float opacity = 1f;
        float opacityIncrement = 0.2f;
        float yeildTime = 0.2f;

        while (opacity > 0f)
        {
            opacity -= opacityIncrement;
            image.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(yeildTime);
        }

        Destroy(gameObject);
    }
}
