using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

public class SaveLoadSystem : MonoBehaviour
{
    private BinaryFormatter formatter;
    FileStream file;
    SaveData saveData;

    private string m_slotName;
    public Transform startPosiiton;

    private void InitializeSaveData()
    {
        formatter = new BinaryFormatter();

        string filePath = Application.dataPath + "/SaveGame/" + m_slotName;
        Debug.Log(filePath);

        file = File.Open(filePath, FileMode.Create);

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

        saveData.startPosition[0] = 3.66f;
        saveData.startPosition[1] = -0.36f;
        saveData.startPosition[2] = -0.36f;

        saveData.faceDirection[0] = false;
        saveData.faceDirection[1] = true;

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

    public bool[] faceDirection = new bool [2];
    public float[] startPosition = new float [3];

    public string sceneName;

    public enumInventory[] inventory = new enumInventory[5];
}