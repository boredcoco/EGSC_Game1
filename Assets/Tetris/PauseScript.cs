using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public Button pause;
    public Text PauseText;
    public GameObject PausePopup;

    void Start()
    {
        Button btn = pause.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        PausePopup.SetActive(false);

    }

    void TaskOnClick()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        PauseText.text = "Pause"; 
        PausePopup.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        PauseText.text = "Resume"; 
        PausePopup.SetActive(true);
    }
}
