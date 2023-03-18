using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class JigsawPuzzleSettings : MonoBehaviour
{
    [SerializeField] private string[] filepaths;
    [SerializeField] private string[] displayText;

    [SerializeField] private TMP_Text textDisplayed;
    [SerializeField] private Image imageDisplayed;

    private string key = "puzzlePicture"; // key used to access PlayerPrefs
    private int currentIndex = 0;
    private string currentFilepath = "";

    private void Start()
    {
      if (filepaths.Length != 0)
      {
        currentFilepath = filepaths[0];
      }
    }

    public void ClickNext()
    {
      if (displayText.Length == 0 || filepaths.Length == 0)
      {
        return;
      }
      if (currentIndex >= displayText.Length || currentIndex >= filepaths.Length)
      {
        currentIndex = 0;
      }
      textDisplayed.text = displayText[currentIndex];

      // Load the imageDisplayed
      Sprite sprite = Resources.Load<Sprite>(filepaths[currentIndex]);
      imageDisplayed.sprite = sprite;
      currentFilepath = filepaths[currentIndex];

      // Increment the currentIndex
      currentIndex += 1;
    }

    public void ClickBack()
    {
      if (displayText.Length < 0 || filepaths.Length < 0)
      {
        return;
      }
      if (currentIndex < 0 || currentIndex < 0)
      {
        currentIndex = Mathf.Min(displayText.Length, filepaths.Length) - 1;
      }
      textDisplayed.text = displayText[currentIndex];

      // Load the imageDisplayed
      Sprite sprite = Resources.Load<Sprite>(filepaths[currentIndex]);
      imageDisplayed.sprite = sprite;
      currentFilepath = filepaths[currentIndex];

      // Increment the currentIndex
      currentIndex -= 1;
    }

    public void ConfirmSelection()
    {
      PlayerPrefs.SetString(key, currentFilepath);
      SceneManager.LoadScene("JigsawPuzzle");
    }
}
