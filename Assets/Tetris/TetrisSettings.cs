using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisSettings : MonoBehaviour
{
    public void gotToSettings()
    {
      SceneManager.LoadScene("Tetris3DSettings");
    }

    public void goToTetrisMainPage()
    {
      SceneManager.LoadScene("Tetris3D");
    }
}
