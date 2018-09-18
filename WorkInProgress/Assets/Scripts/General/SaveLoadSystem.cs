using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    private BinaryFormatter formatter;
    FileStream file;
    SaveData saveData;

    private string m_slotName;

    public Transform startPosiiton;
    public GameObject[] loadButton;

    public string m_path;

    private void Start()
    {
        m_path = Application.dataPath + "/SaveGame/";
    }

    private void InitializeSaveData()
    {
        formatter = new BinaryFormatter();

        string path = m_path + m_slotName + ".dat";

        file = File.Open(path, FileMode.Create);

        saveData = new SaveData();
    }

    private void CheckSaveFiles()
    {
        if(!File.Exists(m_path + "/Slot1.dat"))
        {
            loadButton[0].SetActive(false);
        }

        if (!File.Exists(m_path + "/Slot2.dat"))
        {
            loadButton[1].SetActive(false);
        }

        if (!File.Exists(m_path + "/Slot3.dat"))
        {
            loadButton[2].SetActive(false);
        }
    }

    public void LoadGame(string slotName)
    {
        string path = m_path + slotName + ".dat";

        file = File.Open(path, FileMode.Open);

        saveData = (SaveData)formatter.Deserialize(file);
    }

    private void DoLoad()
    {

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Start")
            CheckSaveFiles();
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