using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToMenuPopup : MonoBehaviour
{
    public GameObject popupWindow;

    private void Start()
    {
        popupWindow.SetActive(false); // Set the pop-up window to be initially closed
    }

    public void ShowPopup()
    {
        popupWindow.SetActive(true);
    }

    public void ReturnToGameStart()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ReturnToGame()
    {
       popupWindow.SetActive(false);  
    }

    public void ClosePopup()
    {
        popupWindow.SetActive(false);
    }
}
