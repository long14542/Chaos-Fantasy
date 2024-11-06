using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // These are the possible states of the game while running
    public enum GameState
    {
        Playing,
        Paused,
        LevelUp
    }
    public GameState currentState;
    
    public static GameManager instance;

    [Header("Screens")]
    public GameObject levelUpScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // Prevent the creation of another instance of this class
        else
        {
            Debug.Log("GameManager duplicate destroyed: " + this);
            Destroy(gameObject);
        }

        DisableScreens();
    }

    void DisableScreens()
    {
        levelUpScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused then resume it
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            // If the game is playing then pause it
            else if (currentState == GameState.Playing)
            {
                PauseGame();
            }
        }

    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        // The scale at which time passes
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
    }

    public void StartLevelUpScreen()
    {
        currentState = GameState.LevelUp;
        Time.timeScale = 0f;
        levelUpScreen.SetActive(true);
    }

    public void EndLevelUpScreen()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
    }
}
