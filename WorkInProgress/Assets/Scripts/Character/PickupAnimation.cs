using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    private GameObject cloneObject;
    private SpriteRenderer spriteRenderer;

    public void SetPickupAnimation(Sprite sprite, float proportion)
    {
        cloneObject = Instantiate(gameObject,
            transform.position,
            transform.rotation );

        spriteRenderer = 
            cloneObject.GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = sprite;

        cloneObject.transform.localScale = new Vector3(proportion, proportion, 0f);

        StartCoroutine(DoPickupAnimation());
    }

    private IEnumerator DoPickupAnimation()
    {
        Vector3 targetPosition = new Vector3
            (cloneObject.transform.position.x, cloneObject.transform.position.y + 100f, 0f);

        float opacity = 1f;
        float opacityIncrement = 0.2f;
        float yeildTime = 0.2f;

        while (opacity > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position, 0.05f * Time.deltaTime);
            
            opacity -= opacityIncrement;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);

            yield return new WaitForSeconds(yeildTime);
        }

        Destroy(cloneObject);
    }
}
