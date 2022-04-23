using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class saveManager : MonoBehaviour
{
    public static saveManager Instance;
    public int BestScore;
    public string bestScoreName;
    public string playerName;
    public float paddleSpeed = 5f;
    public float ballBounce = 1f;

    public List<int> scores;
    public List<string> names;

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
      public string playerName;
      public float paddleSpeed;
      public float ballBounce;
    }

    public void saveData()
    {
      SaveData data = new SaveData();
      data.BestScore = BestScore;
      data.playerName = bestScoreName;
      data.paddleSpeed = paddleSpeed;
      data.ballBounce = ballBounce;

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
        bestScoreName = data.playerName;
        paddleSpeed = data.paddleSpeed;
        ballBounce = data.ballBounce;
      }
    }
}
