using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private MemoryGameSettings settings;
    [SerializeField] private string filepath;

    // handles selection
    [SerializeField] private GameObject selectBubble;

    private bool isChosen = false;

    private void handleSelect()
    {
      if (settings.Select(filepath))
      {
        selectBubble.SetActive(true);
        isChosen = true;
      }
    }

    private void handleDeSelect()
    {
      if (settings.Deselect())
      {
        selectBubble.SetActive(false);
        isChosen = false;
      }
    }

    public void Select()
    {
      if (isChosen)
      {
        handleDeSelect();
      } else
      {
        handleSelect();
      }
    }
}
