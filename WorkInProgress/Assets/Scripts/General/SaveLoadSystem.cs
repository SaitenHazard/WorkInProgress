using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    FileStream file;
    SaveData saveData;
    private BinaryFormatter formatter;

    private string m_slotName;

    public Transform startPosiiton;
    public GameObject[] loadButton;
    public GameObject DontDestory;

    public static SaveLoadSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(DontDestory);

    }

    private void InitializeSaveData()
    {
        formatter = new BinaryFormatter();

        string path = Application.dataPath.ToString() + "/SaveGame/" + 
            m_slotName + ".dat";

        file = File.Open(path, FileMode.Create);

        saveData = new SaveData();
    }

    private void CheckSaveFiles()
    {
        Debug.Log(Application.dataPath.ToString() + "/SaveGame/" + "Slot1.dat");

        string path = Application.dataPath.ToString() + "/SaveGame/";

        if(!File.Exists(path + "/Slot1.dat"))
        {
            loadButton[0].SetActive(false);
        }

        if (!File.Exists(path + "/Slot2.dat"))
        {
            loadButton[1].SetActive(false);
        }

        if (!File.Exists(path + "/Slot3.dat"))
        {
            loadButton[2].SetActive(false);
        }
    }

    public void LoadGame(string slotName)
    {
        formatter = new BinaryFormatter();

        Debug.Log(Application.dataPath.ToString() + "/SaveGame/" + slotName + ".dat");

        string path = Application.dataPath.ToString() + "/SaveGame/" + slotName + ".dat";

        file = File.Open(path, FileMode.Open);
        saveData = (SaveData)formatter.Deserialize(file);
        file.Close();

        DoLoad();
    }

    private void DoLoad()
    {
        PlayerInstant.Instance.GetComponent<PlayerInventory>().
            SetInventoryArray(saveData.inventory);

        PlayerInstant.Instance.GetComponentInChildren<Attackable>().
            SetHealth(saveData.health);

        PlayerInstant.Instance.GetComponent<PlayerWallet>().
            SetCoin(saveData.coin);

        startPosiiton.position = new Vector3(saveData.startPosition[0], 
            saveData.startPosition[1], saveData.startPosition[2]);

        Vector2 faceDirection = new Vector2(saveData.faceDirection[0], 
            saveData.faceDirection[1]);

        m_slotName = saveData.slotName;

        WarpManager.Instance.Warp(saveData.sceneName, "WarpStart", faceDirection);

        SetTitleScreenActive(false);
    }

    private void SetTitleScreenActive(bool active)
    {
        TitleScreenView.Instance.SetActive(active);
    }

    private void SaveFile()
    {
        formatter.Serialize(file, saveData);
        file.Close();
    }

    private void InitializeSave()
    {
        saveData.date = DateTime.Now.ToShortDateString();
        saveData.time = DateTime.Now.ToLongTimeString();

        saveData.coin = PlayerInstant.Instance.GetComponent<PlayerWallet>().GetCoins();
        saveData.health = PlayerInstant.Instance.
            GetComponentInChildren<Attackable>().GetHealth();

        saveData.startPosition[0] = PlayerInstant.Instance.
            GetComponent<Transform>().position.x;
        saveData.startPosition[1] = PlayerInstant.Instance.
            GetComponent<Transform>().position.y;
        saveData.startPosition[2] = PlayerInstant.Instance.
            GetComponent<Transform>().position.z;

        saveData.faceDirection[0] = PlayerInstant.Instance.
            GetComponent<CharacterMovementModel>().GetFacingDirection().x;
        saveData.faceDirection[1] = PlayerInstant.Instance.
            GetComponent<CharacterMovementModel>().GetFacingDirection().y;

        saveData.inventory = PlayerInstant.Instance.GetComponent<PlayerInventory>().
            GetEntireInventory();

        saveData.slotName = m_slotName;
        saveData.sceneName = SceneManager.GetActiveScene().name;
    }

    public void DoSaveGame()
    {
        InitializeSaveData();
        InitializeSave();
        SaveFile();
    }

    public void DoNewGame(string slotName)
    {
        m_slotName = slotName;

        InitializeSaveData();
        InitializeNewSave();
        SaveFile();
        DoLoad();
    }

    private void InitializeNewSave()
    {
        saveData.date = DateTime.Now.ToShortDateString();
        saveData.time = DateTime.Now.ToLongTimeString();

        saveData.coin = 0;
        saveData.health = 10;

        saveData.startPosition[0] = 3.66f;
        saveData.startPosition[1] = -0.36f;
        saveData.startPosition[2] = -0.36f;

        saveData.faceDirection[0] = 0;
        saveData.faceDirection[1] = -1;

        saveData.slotName = m_slotName;
        saveData.sceneName = "WorkInProgress";

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

    public int coin;
    public int health;

    public float[] faceDirection = new float[2];
    public float[] startPosition = new float [3];

    public string sceneName;

    public enumInventory[] inventory = new enumInventory[5];
}