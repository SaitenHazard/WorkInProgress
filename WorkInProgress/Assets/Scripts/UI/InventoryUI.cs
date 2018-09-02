using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public PlayerInventory m_Inventory;
    public GameObject selectedSlot;

    void Update ()
    {
        UpdateSlotSelected();
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

        string slotName = getSlotName(selectedSlotID);
        Debug.Log(slotName);
        GameObject slotObject = transform.Find(slotName).gameObject;

        selectedSlot.transform.position = slotObject.transform.position;
    }

    private string getSlotName(int index)
    {
        string slotName = "Slot" + index.ToString();
        return slotName;
    }
}
