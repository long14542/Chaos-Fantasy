using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // These are the possible states of the game while running
    public enum GameState
    {
        Playing,
        Paused
    }
    public GameObject pauseMenuUI;
    public Button pauseButton;
    public GameState currentState;
    void Start()
    {
        currentState = GameState.Playing;
        pauseMenuUI.SetActive(false);  // Hide pause menu initially
        pauseButton.onClick.AddListener(TogglePause);
    }


    void Update()
    {
        {
            // Check for Escape key press only
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
        public void TogglePause()
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else if (currentState == GameState.Playing)
            {
                PauseGame();
            }
        }

        public void PauseGame()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Restart current scene
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");  // Make sure the main menu scene is set in the build settings
    }

    public void AdjustSound(float volume)
    {
        AudioListener.volume = volume;  // Set this based on a UI slider
    }
}