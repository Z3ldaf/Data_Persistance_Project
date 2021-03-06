using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 12;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public TextMeshProUGUI bestPlayer;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    public Animator transition;
    public float waitingTime = 1f;

    private string bestScoreName = saveManager.Instance.bestScoreName;
    private string playerName = saveManager.Instance.playerName;


    void Awake()
    {
      int BestScore = saveManager.Instance.BestScore;

      BestScoreText.text = $"Best Score: {BestScore}";
      bestPlayer.text = "Name: " + bestScoreName;
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.7f;
        int perLine = Mathf.FloorToInt(10.0f / step);

        int[] pointCountArray = new [] {1,1,2,2,3,3,4,4,5,5,10,10};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-4.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(QuitAndSave(0));
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public IEnumerator QuitAndSave(int index)
    {
      if(m_Points > saveManager.Instance.BestScore)
      {
          saveManager.Instance.BestScore = m_Points;
          saveManager.Instance.bestScoreName = playerName;
          saveManager.Instance.scores.Add(m_Points);
          saveManager.Instance.names.Add(bestScoreName);
          saveManager.Instance.saveData();
      }
      else
      {
          saveManager.Instance.scores.Add(m_Points);
          saveManager.Instance.names.Add(playerName);
      }

      transition.SetTrigger("start");
      yield return new WaitForSeconds(waitingTime);
      SceneManager.LoadScene(index);
    }
}
