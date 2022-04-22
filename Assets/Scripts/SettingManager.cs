using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Animator transition;
    public float waitingTime = 1f;

    public Slider setPaddleSpeed;
    public Slider setBallBounce;

    private void Awake()
    {
      setPaddleSpeed.value = saveManager.Instance.paddleSpeed;
      setBallBounce.value = saveManager.Instance.ballBounce;
    }

    public void BackToMenu()
    {
      StartCoroutine(loadScene(0));
    }

    public void SaveAndQuit()
    {
      saveManager.Instance.paddleSpeed = setPaddleSpeed.value;
      saveManager.Instance.ballBounce = setBallBounce.value;

      Debug.Log(saveManager.Instance.paddleSpeed);
      Debug.Log(saveManager.Instance.ballBounce);

      StartCoroutine(loadScene(0));
    }

    public IEnumerator loadScene(int index)
    {
      transition.SetTrigger("start");
      yield return new WaitForSeconds(waitingTime);
      SceneManager.LoadScene(index);
    }
}
