using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public static InventoryUI Instance;

    public PlayerInventory m_Inventory;
    public GameObject selectedSlot;

    private enumInventory[] inventory;
    private int selectedSlotID;

    private void Awake()
    {
        inventory = new enumInventory[m_Inventory.GetInventoryMaxSize()];
        Instance = this;
    }

    protected void Update ()
    {
        UpdateSlot();
        UpdateSelected();
    }

    private void UpdateSelected()
    {
        selectedSlotID = m_Inventory.getSelectedID();

        UpdateSlotSelected();
        UpdateQuickInventoryText();
    }

    private void UpdateQuickInventoryText()
    {
        Text text = GetComponentInChildren<Text>();

        if (selectedSlotID == -1)
        {
            selectedSlot.SetActive(false);
            text.text = "";
        }

        if (m_Inventory.GetSelectedItem() == enumInventory.HealthPickup)
        {
            text.text = "Claw of Health";
        }

        if (m_Inventory.GetSelectedItem() == enumInventory.DecoyPickup)
        {
            text.text = "Duplication Clay";
        }

        //if (m_Inventory.GetSelectedItem() == enumInventory.InvisiblePickup)
        //{
        //    text.text = "Claw of Invisibility";
        //}

        if (m_Inventory.GetSelectedItem() == enumInventory.ProjectilePickup)
        {
            text.text = "Claw of Projectile";
        }

        if (m_Inventory.GetSelectedItem() == enumInventory.RangePickup)
        {
            text.text = "Range Potion";
        }

        if (m_Inventory.GetSelectedItem() == enumInventory.SpeedPickup)
        {
            text.text = "Speed Potion";
        }

        if (m_Inventory.GetSelectedItem() == enumInventory.StrengthPickup)
        {
            text.text = "Strenght Potion";
        }

        if (m_Inventory.GetSelectedItem() == enumInventory.SlimePickup)
        {
            text.text = "Slime";
        }
    }

    private void UpdateSlot()
    {
        inventory = m_Inventory.GetEntireInventory();

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

    private enumInventory GetItemFromSlot(int index)
    {
        return inventory[index];
    }

    private void SetItemSlot(int index, GameObject itemSlotObject)
    {
        Image itemSlotImage = itemSlotObject.GetComponent<Image>();
        GameObject itemObject = Resources.Load(GetItemFromSlot(index).ToString()) as GameObject;
        SpriteRenderer spriteRenderer = itemObject.GetComponentInChildren<SpriteRenderer>();

        itemSlotObject.SetActive(true);
        itemSlotImage.sprite = spriteRenderer.sprite;
    }

    private void UpdateSlotSelected()
    {
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
