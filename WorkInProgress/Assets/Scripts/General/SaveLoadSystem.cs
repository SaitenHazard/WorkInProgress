using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;

    private string slotName;

    void Awake()
    {
        Instance = this;
    }

    public void SetSlotName(string index)
    {
        slotName = "Slot" + index + ".dat";
    }

    private void Start()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "SaveGame" + 
            slotName, FileMode.Create);

        SaveData data = new SaveData();

        formatter.Serialize(file, data);
        file.Close();
    }
}


[Serializable]
class SaveData
{
    String date;
    String time;

    public SaveData()
    {
        date = DateTime.Now.ToShortDateString();
        time = DateTime.Now.ToLongTimeString();
    }
}
