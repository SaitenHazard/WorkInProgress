using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    private static int maxInventorySize = 5;
    private int selectedSlotID = -1;
    private int inventorySize = 0;

    private enumInventory [] inventoryArray = new enumInventory[maxInventorySize];

    public void Start()
    {
        instance = this;
        InitializeInventory();
    }

    public void InitializeInventory()
    {
        for (int i = 0; i < maxInventorySize; i++)
            inventoryArray[i] = enumInventory.NULL;
    }

    public void AddItem(enumInventory item)
    {
        int firstEmptySlot = GetFirstEmptySlot();

        inventoryArray[firstEmptySlot] = item;

        inventorySize++;
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
        return inventoryArray[selectedSlotID];
    }

    public void changeSelectedSlotID(bool forward)
    {
        if (forward)
        {
            selectedSlotID++;

            if (selectedSlotID == maxInventorySize -1)
                selectedSlotID = 0;
        }
        else
        {
            selectedSlotID--;

            if (selectedSlotID == -1)
                selectedSlotID = maxInventorySize -1;
        }
    }
}