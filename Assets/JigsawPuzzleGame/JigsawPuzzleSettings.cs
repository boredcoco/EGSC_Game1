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

    public void ClickNext()
    {
      if (displayText.Length == 0 || filepaths.Length == 0)
      {
        return;
      }
      // Increment the currentIndex
      currentIndex += 1;

      if (currentIndex >= displayText.Length || currentIndex >= filepaths.Length)
      {
        currentIndex = 0;
      }
      textDisplayed.text = displayText[currentIndex];

      // Load the imageDisplayed
      Sprite sprite = Resources.Load<Sprite>(filepaths[currentIndex]);
      imageDisplayed.sprite = sprite;
    }

    public void ClickBack()
    {
      if (displayText.Length == 0 || filepaths.Length == 0)
      {
        return;
      }
      // Decrement the currentIndex
      currentIndex -= 1;

      if (currentIndex < 0 || currentIndex < 0)
      {
        currentIndex = Mathf.Min(displayText.Length, filepaths.Length) - 1;
      }
      textDisplayed.text = displayText[currentIndex];

      // Load the imageDisplayed
      Sprite sprite = Resources.Load<Sprite>(filepaths[currentIndex]);
      imageDisplayed.sprite = sprite;
    }

    public void ConfirmSelection()
    {
      PlayerPrefs.SetString(key, filepaths[currentIndex]);
      SceneManager.LoadScene("JigsawPuzzle");
    }
}
