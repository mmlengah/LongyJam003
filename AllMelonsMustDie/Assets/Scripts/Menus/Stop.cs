using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stop : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject ui;

    public static TMP_Text tmpText;

    private bool isPaused = false;

    private void Start()
    {
        tmpText = ui.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        pauseMenu.SetActive(false);
        ui.SetActive(true);
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
        ui.SetActive(false); 
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Resume the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Hide the mouse cursor
        pauseMenu.SetActive(false); // Hide the pause menu
        ui.SetActive(true);
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
