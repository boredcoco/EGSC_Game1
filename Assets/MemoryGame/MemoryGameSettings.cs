using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryGameSettings : MonoBehaviour
{
    [SerializeField] private int totalFields = 3;

    private int numberOfFieldsSelected = 0;

    public bool Select(string filepath)
    {
      if (numberOfFieldsSelected >= totalFields) // max selected
      {
        return false;
      }
      string key = "spriteSelected" + numberOfFieldsSelected.ToString();
      PlayerPrefs.SetString(key, filepath);
      numberOfFieldsSelected += 1;
      return true;
    }

    public bool Deselect()
    {
      if (numberOfFieldsSelected <= 0) // max selected
      {
        return false;
      }
      string key = "spriteSelected" + numberOfFieldsSelected.ToString();
      PlayerPrefs.SetString(key, "None Selected");
      numberOfFieldsSelected -= 1;
      return true;
    }

    public void GoToMemoryGame()
    {
      if (numberOfFieldsSelected == totalFields)
      {
        SceneManager.LoadScene("MemoryGame");
      }
    }
}
