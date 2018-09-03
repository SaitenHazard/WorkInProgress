using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    private GameObject cloneObject;
    private SpriteRenderer spriteRenderer;

    public void DoAnimation(Sprite sprite, float proportion)
    {
        SetPickupAnimation(sprite, proportion);
        StartAnimation();
    }

    private void StartAnimation()
    {
        PickupAnimation clonePickupAnimation = cloneObject.GetComponent<PickupAnimation>();
        StartCoroutine(clonePickupAnimation.Animate());
    }

    private void SetPickupAnimation(Sprite sprite, float proportion)
    {
        cloneObject = Instantiate(gameObject, transform.position, transform.rotation );

        spriteRenderer = cloneObject.GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = sprite;
        cloneObject.transform.localScale = new Vector3(proportion, proportion, 0f);
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Debug.Log("Do: " + m_spriteRenderer);

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
