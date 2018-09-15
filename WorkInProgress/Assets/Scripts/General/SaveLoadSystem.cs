using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

public class SaveLoadSystem : MonoBehaviour
{
    private string m_slotName;
    private BinaryFormatter formatter;
    FileStream file;
    SaveData saveData;

    private void InitializeSaveData()
    {
        formatter = new BinaryFormatter();

        file = File.Open(Application.dataPath + "SaveGame" +
            GetSlotName(), FileMode.Create);

        saveData = new SaveData();
    }

    public string GetSlotName()
    {
        string st = null;
        return st;
    }

    private void SaveGame()
    {
        formatter.Serialize(file, saveData);
        file.Close();
    }

    public void DoNewGameSave(string slotName)
    {
        m_slotName = slotName;
        InitializeSaveData();
        InitializeNewSave();
        SaveGame();
    }

    private void InitializeNewSave()
    {
        saveData.date = DateTime.Now.ToShortDateString();
        saveData.time = DateTime.Now.ToLongTimeString();

        saveData.money = 0;
        saveData.life = 10;

        saveData.startPosition = new Vector3(3.66f, -0.36f, -0.36f);
        saveData.faceDirection = new Vector2(0, -1);

        saveData.slotName = m_slotName;

        for (int i = 0; i < 5; i++)
            saveData.inventory[i] = enumInventory.NULL;
    }
}

[Serializable]
class SaveData
{
    public string slotName;

    public string date;
    public string time;

    public int money;
    public int life;

    public Vector2 faceDirection;
    public Vector3 startPosition;

    public string sceneName;

    public enumInventory[] inventory = new enumInventory[5];
}