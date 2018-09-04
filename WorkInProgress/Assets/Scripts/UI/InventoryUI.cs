using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public static InventoryUI Instance;

    public PlayerInventory m_Inventory;
    public GameObject selectedSlot;

    private enumInventory[] inventory;

    private void Awake()
    {
        inventory = new enumInventory[m_Inventory.GetInventoryMaxSize()];
        Instance = this;
    }

    private void Update ()
    {
        UpdateSlot();
        UpdateSlotSelected();
    }

    private void UpdateSlot()
    {
        inventory = m_Inventory.getEntireInventory();

        for(int i = 0; i < m_Inventory.GetInventoryMaxSize(); i++)
        {
            GameObject slotObject = getSlotObject(i);
            GameObject itemSlotObject = slotObject.transform.GetChild(0).gameObject;

            if (inventory[i] != enumInventory.NULL)
            {
                SetItemSlot(i, itemSlotObject);
            }
            else
            {
                itemSlotObject.SetActive(false);
            }
        }
    }

    private void SetItemSlot(int index, GameObject itemSlotObject)
    {
        Image itemSlotImage = itemSlotObject.GetComponent<Image>();
        GameObject itemObject = Resources.Load(inventory[index].ToString()) as GameObject;
        SpriteRenderer spriteRenderer = itemObject.GetComponentInChildren<SpriteRenderer>();

        itemSlotObject.SetActive(true);
        itemSlotImage.sprite = spriteRenderer.sprite;
    }

    private void UpdateSlotSelected()
    {
        int selectedSlotID = m_Inventory.getSelectedID();

        if(selectedSlotID == -1)
        {
            selectedSlot.SetActive(false);
            return;
        }

        selectedSlot.SetActive(true);
        GameObject slotObject = getSlotObject(selectedSlotID);
        selectedSlot.transform.position = slotObject.transform.position;
    }

    public RectTransform GetSlotSelectedRectTransform()
    {
        GameObject Object = getSlotObject(m_Inventory.getSelectedID());

        RectTransform rectTransform = Object.GetComponent<RectTransform>();

        return rectTransform;
    }

    private GameObject getSlotObject(int index)
    {
        string slotName = "Slot" + index.ToString();
        GameObject slotObject = transform.Find(slotName).gameObject;
        return slotObject;
    }
}
