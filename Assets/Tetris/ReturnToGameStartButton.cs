using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToGameStartButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ReturnToGameStart);
    }

    private void ReturnToGameStart()
    {
        SceneManager.LoadScene("GameStart");
    }
}
