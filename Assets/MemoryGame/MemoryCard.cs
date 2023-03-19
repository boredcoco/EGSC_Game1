using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameLogicManager gameLogicManager;
    private GameObject picture;

    private string uniqueId = null;
    private bool isFlipped = false;

    public void Initialize(string id, GameObject pic)
    {
      uniqueId = id;
      picture = pic;
      picture.transform.position = transform.position;
      picture.SetActive(false);
    }

    private void handleOpen()
    {
      if (gameLogicManager.FlipCard(uniqueId))
      {
        isFlipped = true;
        picture.SetActive(true);
      }
    }

    private void handleClose()
    {
      if (gameLogicManager.UnflipCard(uniqueId))
      {
        isFlipped = false;
        picture.SetActive(false);
      }
    }

    private void OnMouseDown()
    {
        if (!isFlipped)
        {
          handleOpen();
        } else {
          handleClose();
        }
    }
}
