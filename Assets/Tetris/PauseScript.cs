using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public Button pause;
    public Text PauseText;

    void Start()
    {
        Button btn = pause.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
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
    }

    void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        PauseText.text = "Resume"; // Change the button text to "Resume"
    }
}
