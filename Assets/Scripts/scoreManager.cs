using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class scoreManager : MonoBehaviour
{
  public Animator transition;
  public float waitingTime = 1f;

  public TextMeshProUGUI scoresText;
  public TextMeshProUGUI namesText;

  private void Awake()
  {
    scoresText.text = "";
    namesText.text = "";
  }

  public void SetText()
  {
    int[] scores = saveManager.Instance.scores.ToArray();
    string[] names = saveManager.Instance.names.ToArray();

    IntArrayToString(scores, scoresText);
    StringArrayToString(names, namesText);
  }

  public void BackToMenu(){
    StartCoroutine(loadScene(0));
  }

  private void IntArrayToString(int[] intArray, TextMeshProUGUI text)
  {
    if(intArray != null)
    {
      for(int i = 0; i < intArray.Length; i++)
      {
        text.text = text.text + "\n" + intArray[i].ToString();
      }
    }
  }

  private void StringArrayToString(string[] stringArray, TextMeshProUGUI text)
  {
    if(stringArray != null)
    {
      for(int i = 0; i < stringArray.Length; i++)
      {
        text.text = text.text + "\n" + stringArray[i].ToString();
      }
    }
  }

  public IEnumerator loadScene(int index)
  {
    transition.SetTrigger("start");
    yield return new WaitForSeconds(waitingTime);
    SceneManager.LoadScene(index);
  }
}
