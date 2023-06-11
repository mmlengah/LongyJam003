using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the mouse cursor
        pauseMenu.SetActive(true); // Show the pause menu
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Hide the mouse cursor
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    public void ResumeButton()
    {
        ResumeGame();
    }

    public void ExitButton()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("Main Menu"); // Load the main menu scene
    }
}
