using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;

    void Awake()
    {
        Instance = this;
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
        FileStream file = File.Open(Application.dataPath + "/save.dat", FileMode.Create);

        SaveData data = new SaveData();

        formatter.Serialize(file, data);
        file.Close();

        Debug.Log("Saved");
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
