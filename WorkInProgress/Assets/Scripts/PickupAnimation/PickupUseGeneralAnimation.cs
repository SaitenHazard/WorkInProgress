using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupUseGeneralAnimation : MonoBehaviour {

    public GameObject objectToClone;
    private GameObject cloneObject;
    private Image image;

    private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = GetComponent<InventoryUI>();
    }

    public void DoAnimation(Sprite sprite, float proportion, RectTransform rectTransform)
    {
        SetPickupAnimation(sprite, proportion, rectTransform);
        StartUseAnimation();
    }

    protected void StartUseAnimation()
    {
        PickupInventoryAnimation clonePickupAnimation = cloneObject.GetComponent<PickupInventoryAnimation>();
        StartCoroutine(clonePickupAnimation.UseAnimate());
    }

    public void DoDestoryAnimation(Sprite sprite, float proportion, RectTransform rectTransform)
    {
        SetPickupAnimation(sprite, proportion, rectTransform);
        StartUseAnimation();
    }

    virtual protected void SetPickupAnimation(Sprite sprite, float proportion, RectTransform slotRectTransform)
    {
        cloneObject = Instantiate(objectToClone);
        cloneObject.transform.SetParent(gameObject.transform);

        RectTransform rectTransform = cloneObject.GetComponent<RectTransform>();

        image = cloneObject.GetComponent<Image>();
        image.sprite = sprite;
        image.rectTransform.localScale = new Vector2 (proportion, proportion);
        image.rectTransform.position = slotRectTransform.position;

        image.enabled = true;
    }
}