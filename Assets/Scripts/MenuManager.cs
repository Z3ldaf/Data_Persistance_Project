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

    public void Awake()
    {
      BestScoreText.text = "Best Score: " + saveManager.Instance.BestScore;
    }

    public void PlayGame()
    {
      StartCoroutine(loadScene(1));
    }

    public void QuitGame()
    {
      Application.Quit();
    }

    public IEnumerator loadScene(int index)
    {
      transition.SetTrigger("start");
      yield return new WaitForSeconds(waitingTime);
      SceneManager.LoadScene(index);
    }
}
