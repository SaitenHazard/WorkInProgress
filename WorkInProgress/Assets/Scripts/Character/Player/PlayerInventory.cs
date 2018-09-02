using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    public static int maxInventorySize = 3;
    private int selectedSlotID = -1;
    private int countNumberOfItems = 0;

    private enumInventory [] inventoryArray = new enumInventory[maxInventorySize];

    public void Start()
    {
        instance = this;
    }

    public void AddItem(enumInventory itemType, int amount)
    {
        for (int i = 0; i < maxInventorySize; i++)
        {
            AddItem(itemType);
        }
    }

    public enumInventory[] getEntireInventory()
    {
        return inventoryArray;
    }

    public int getSelectedID()
    {
        return selectedSlotID;
    }

    public enumInventory GetSelectedItem()
    {
        if (selectedSlotID == -1) return enumInventory.NULL;

        return inventoryArray[selectedSlotID];
    }

    public void changeSelectedSlotID(bool forward)
    {
        if (countNumberOfItems == 0)
        {
            selectedSlotID = -1;
            return;
        }

        if (forward)
        {
            if (selectedSlotID == maxInventorySize - 1)
                selectedSlotID = 0;
            else
                selectedSlotID++;

            if (inventoryArray[selectedSlotID] == enumInventory.NULL)
            {
                changeSelectedSlotID(true);
            }
        }
        else
        {
            if (selectedSlotID == 0)
                selectedSlotID = maxInventorySize - 1;
            else
                selectedSlotID--;

            if (inventoryArray[selectedSlotID] == enumInventory.NULL)
                changeSelectedSlotID(false);
        }
    }

    public int GetNumberOfItems()
    {
        return countNumberOfItems;
    }

    public enumInventory getInventoryItem(int index)
    {
        return inventoryArray[index];
    }

    public void AddItem(enumInventory itemType)
    {
        int emptyIndex = findFirstEmptyIndex();

        if (emptyIndex == -1)
        {
            return;
        }

        inventoryArray[emptyIndex] = itemType;
        countNumberOfItems++;

        if (countNumberOfItems == 1) changeSelectedSlotID(true);
    }

    public void RemoveSelectedItem()
    {
        RemoveItem(selectedSlotID);
    }

    private void RemoveItem(int index)
    {
        inventoryArray[index] = enumInventory.NULL;
        countNumberOfItems--;

        changeSelectedSlotID(true);
    }

    public int GetMaxSize()
    {
        return maxInventorySize;
    }

    private int findFirstEmptyIndex()
    {
        for ( int i = 0; i < maxInventorySize; i++)
        {
            if (inventoryArray[i] == enumInventory.NULL)
            {
                return i;
            }
                
        }

        return -1;
    }
}