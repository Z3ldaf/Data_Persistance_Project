using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public Animator transition;
    public float waitingTime = 1f;

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
