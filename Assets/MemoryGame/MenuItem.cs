using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private MemoryGameSettings settings;
    [SerializeField] private string filepath;

    private Image image;
    private bool isChosen = false;

    private void Start()
    {
      image = GetComponent<Image>();
    }

    private void handleSelect()
    {
      if (settings.Select(filepath))
      {
        image.color = Color.blue;
        isChosen = true;
      }
    }

    private void handleDeSelect()
    {
      if (settings.Deselect())
      {
        image.color = Color.white;
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
