// *---- Purpose of this file: ----*
// Manage to which scene the user gets send, depending on what button is
// pressed.

// Based on the tutorial "Unity VR Development for Oculus Quest: Main Menu"
// by 'Realary' on youtube:
// https://www.youtube.com/watch?v=Xhz7cW5dbyY&t=73s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public LevelManager levelManager;

    void Start()
    {
        Time.timeScale = 1;
    }

    public bool gamePaused = false;

    // Startbutton loads the main scene
    public void StrtBtn()
    {
        SceneManager.LoadScene("Game");
    }

    // Pause the game, by clicking on the wrist menu button
    public void pauseGame()
    {
        gamePaused = true;
    }

    // Continues the game from the pause menu
    public void continueGame()
    {
        gamePaused = false;
    }

    // Sends the player back to main menu.
    public void MenuBtn()
    {
        SceneManager.LoadScene("Menu");
    }


    // Starts the next level.
    public void StrtLvl()
    {
        levelManager.levelStarted = true;
    }
}
