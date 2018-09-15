using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

public class SaveLoadSystem : MonoBehaviour
{
    private string slotName;
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

    private void SetNewGameData()
    {
        saveData.date = DateTime.Now.ToShortDateString();
        saveData.time = DateTime.Now.ToLongTimeString();

        saveData.money = 0;
        saveData.life = 10;

        saveData.faceDirection = new Vector2(0, -1);
    }
}

[Serializable]
class SaveData
{
    public string saveName;

    public string date;
    public string time;

    public int money;
    public int life;

    public Vector2 faceDirection;
    public GameObject startPosition;

    enumInventory[] inventory = new enumInventory[5];
}