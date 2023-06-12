using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    //Function to load the game scene
    public void LoadGameScene()
    {
        //Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene("Le game");
    }

    //Function to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
