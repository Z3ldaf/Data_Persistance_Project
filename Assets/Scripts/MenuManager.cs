using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public Animator transition;
    public float waitingTime = 1f;

    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI bestPlayer;
    public TMP_InputField playerName;
    public GameObject popUp;

    public void Awake()
    {
      int BestScore = saveManager.Instance.BestScore;
      string bestScoreName = saveManager.Instance.bestScoreName;

      BestScoreText.text = $"Best Score: {BestScore}";
      bestPlayer.text = "Name: " + bestScoreName;
    }

    public void PlayGame()
    {
      if(playerName.text != "")
        {
          StartCoroutine(loadScene(1));
        }
      else
      {
        popUp.SetActive(true);
      }
    }

    public void GoToSetting()
    {
      StartCoroutine(loadScene(2));
    }

    public void QuitGame()
    {
      saveManager.Instance.saveData();
      Application.Quit();
    }

    public void ClosePopUp()
    {
      popUp.SetActive(false);
    }

    public IEnumerator loadScene(int index)
    {
      saveManager.Instance.bestScoreName = playerName.text;
      transition.SetTrigger("start");
      yield return new WaitForSeconds(waitingTime);
      SceneManager.LoadScene(index);
    }
}
