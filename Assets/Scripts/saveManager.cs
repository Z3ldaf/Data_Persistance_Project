using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class saveManager : MonoBehaviour
{
    public static saveManager Instance;
    public int BestScore;

    private void Awake()
    {
      if(Instance != null)
      {
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
      loadData();
    }

    [System.Serializable]
    class SaveData
    {
      public int BestScore;
    }

    public void saveData(int ScoreToSave)
    {
      SaveData data = new SaveData();
      data.BestScore = ScoreToSave;

      BestScore = data.BestScore;

       string json = JsonUtility.ToJson(data);
       File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void loadData()
    {
      string path = Application.persistentDataPath + "/savefile.json";
      if(File.Exists(path))
      {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        BestScore = data.BestScore;
      }
    }
}