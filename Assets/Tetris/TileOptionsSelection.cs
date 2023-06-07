using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileOptionsSelection : MonoBehaviour
{
    [SerializeField] private string pieceName = "";
    // [SerializeField] private Image image;
    private Image image;

    private void Start()
    {
      image = GetComponent<Image>();

      if (PlayerPrefs.HasKey(pieceName))
      {
        image.color = Color.green;
      }
    }

    public void selectTetrisTile()
    {
      if (PlayerPrefs.HasKey(pieceName))
      {
        PlayerPrefs.DeleteKey(pieceName);
        image.color = Color.white;
      }
      else
      {
        PlayerPrefs.SetString(pieceName, "true");
        image.color = Color.green;
      }
    }

}
