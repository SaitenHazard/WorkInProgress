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

    public void DoAnimation(Sprite sprite, float proportion, Transform tranform)
    {
        SetPickupAnimation(sprite, proportion, tranform);
        StartAnimation();
    }

    protected void StartAnimation()
    {
        PickupInventoryAnimation clonePickupAnimation = cloneObject.GetComponent<PickupInventoryAnimation>();
        StartCoroutine(clonePickupAnimation.Animate());
    }

    virtual protected void SetPickupAnimation(Sprite sprite, float proportion, Transform tranform)
    {
        cloneObject = Instantiate(objectToClone, transform.position, transform.rotation);

        image = cloneObject.GetComponentInChildren<Image>();

        image.sprite = sprite;
        cloneObject.transform.localScale = new Vector3(proportion, proportion, 0f);
    }
}
