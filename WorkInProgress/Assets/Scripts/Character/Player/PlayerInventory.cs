using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static int maxInventorySize = 5;
    private static int inventorySize = 0;
    private static int selectedSlotID = -1;
    private static enumInventory [] inventoryArray = new enumInventory[maxInventorySize];

    public void Start()
    {
        InitializeInventory();
    }
 
    public void InitializeInventory()
    {
        for (int i = 0; i < maxInventorySize; i++) inventoryArray[i] = enumInventory.NULL;
    }

    public void AddItem(enumInventory item)
    {
        int firstEmptySlot = GetFirstEmptySlot();
        inventoryArray[firstEmptySlot] = item;
        inventorySize++;

        if(inventorySize == 1) InitializeSelected();
    }

    public void UseSelected()
    {
        if (selectedSlotID == -1)
            return;

        UsePickup();
    }

    public void DestorySelected()
    {
        ResetSlected();
    }

    public void SetInventoryArray(enumInventory [] l_inventoryArray)
    {
        if (selectedSlotID == -1)
            return;

        inventoryArray = l_inventoryArray;
    }

    public void ResetSlected()
    {
        if (selectedSlotID == -1)
            return;

        inventoryArray[selectedSlotID] = enumInventory.NULL;
        inventorySize--;

        changeSelectedSlotID(true);
    }

    private void UsePickup()
    {
        GameObject pickupObject =  Resources.Load<GameObject>("Drops/" + inventoryArray[selectedSlotID].ToString());

        BasePickup basePickup = pickupObject.GetComponent<BasePickup>();

        basePickup.UsePickup();
    }

    public void InitializeSelected()
    {
        selectedSlotID = 0;
    }

    public int GetInventoryMaxSize()
    {
        return maxInventorySize;
    }

    public int GetInventorySize()
    {
        return inventorySize;
    }

    private int GetFirstEmptySlot()
    {
        for( int i = 0; i < maxInventorySize; i++)
        {
            if (inventoryArray[i] == enumInventory.NULL)
                return i;
        }

        return 0;
    }

    public enumInventory[] GetEntireInventory()
    {
        return inventoryArray;
    }

    public int getSelectedID()
    {
        return selectedSlotID;
    }

    public enumInventory GetSelectedItem()
    {
        if (selectedSlotID == -1)
            return enumInventory.NULL;

        return inventoryArray[selectedSlotID];
    }

    public void changeSelectedSlotID(bool forward)
    {
        if (inventorySize == 0)
        {
            selectedSlotID = -1;
            return;
        }

        if (forward)
        {
            selectedSlotID++;

            if (selectedSlotID == maxInventorySize)
                selectedSlotID = 0;

            if (inventoryArray[selectedSlotID] == enumInventory.NULL)
                changeSelectedSlotID(true);
        }
        else
        {
            selectedSlotID--;

            if (selectedSlotID == -1)
                selectedSlotID = maxInventorySize -1;

            if (inventoryArray[selectedSlotID] == enumInventory.NULL)
                changeSelectedSlotID(false);
        }
    }
}