using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartHandler : MonoBehaviour
{
    public void LoadMemoryGame()
    {
      SceneManager.LoadScene("MemoryGame");
    }
}
