using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class TitleScreenView : MonoBehaviour
{
    public static TitleScreenView Instance;

    public GameObject[] Slots;
    public Transform selectedTranform;

    private int indexVertical;
    private int indexHorizontal;

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
        indexVertical = 0;
        indexHorizontal = 0;
    }

    public void ChangeIndex(bool up, bool right)
    {
        if (up)
        {
            indexVertical--;

            if (indexVertical == -1)
                indexVertical = 2;
        }
        else
        {
            indexVertical++;

            if (indexVertical == 3)
                indexVertical = 0;
        }

        if (right)
        {
            indexHorizontal--;

            if (indexHorizontal == -1)
                indexHorizontal = 1;
        }
        else
        {
            indexHorizontal++;

            if (indexHorizontal == 2)
                indexHorizontal = 0;
        }

        UpdateSelected();
    }

    private void UpdateSelected()
    {
        Transform []childTransforms = Slots[indexVertical].GetComponentsInChildren<Transform>();

        selectedTranform.position = childTransforms[indexHorizontal].position;
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
