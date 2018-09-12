using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;

    public GameObject[] Slots;
    private int index;

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
        index = 1;
    }

    public void ChangeIndex(bool increment)
    {
        if (increment)
        {
            index--;

            if (index == 0)
                index = 4;
        }
        else
        {
            index++;

            if (index == 5)
                index = 1;
        }

        UpdateSelected();
    }

    private void UpdateSelected()
    {
        //slected.transform.position = mainMenuTexts[mainMenuIndex].transform.position;

        //for (int i = 0; i < 4; i++)
        //{
        //    subMenuObjects[i].SetActive(false);
        //}

        //subMenuObjects[mainMenuIndex - 1].SetActive(true);
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
