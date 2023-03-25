using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameLogicManager gameLogicManager;
    private GameObject picture;

    private string uniqueId = null;
    private bool isFlipped = false;

    // flipping animations
    private Animator anim;

    private void Start()
    {
      anim = GetComponent<Animator>();
    }

    public void Initialize(string id, GameObject pic)
    {
      uniqueId = id;
      picture = pic;
      picture.transform.position = transform.position;
      picture.SetActive(false);
    }

    private void setPicActive()
    {
      picture.SetActive(true);
    }

    private void setPicInactive()
    {
      picture.SetActive(false);
    }

    private void handleOpen()
    {
      if (gameLogicManager.FlipCard(uniqueId))
      {
        anim.ResetTrigger("isFlip");
        anim.SetTrigger("isFlip");
        isFlipped = true;
        Invoke("setPicActive", 0.3f);
      }
    }

    private void handleClose()
    {
      if (gameLogicManager.UnflipCard(uniqueId))
      {
        anim.ResetTrigger("isFlip");
        anim.SetTrigger("isFlip");
        isFlipped = false;
        Invoke("setPicInactive", 0.3f);
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

/*
    private void FixedUpdate()
    {
      if (isFlipped)
      {
        picture.SetActive(true);
      } else
      {
        picture.SetActive(false);
      }
    }
*/
}
