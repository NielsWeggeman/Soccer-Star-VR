// *---- Purpose of this file: ----*
// Manage the progress between levels.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{

    public KickBall kickBall;
    public LevelManager levelManager;
    public MenuManager menuManager;

    public float minSpeed;
    public float maxSpeed;
    public float maxSpin;
    public float timeToNextShot;

    public int ballsToFire;
    public int bombsToFire;

    public int level = 1;
    public int amountOfLevels = 6;

    public string specialMessage = "";

    public void levelStarted()
    {
        // Different game settings depending on the level the player is
        // currently in.

        switch (level)
        {
            case 1:
                specialMessage = "Welcome to Soccer Star VR!";
                kickBall.startPosition = new Vector3(0f, 0.5f, 0f);
                levelManager.timeToNextShot = 5f;
                kickBall.minSpeed = 10f;
                kickBall.maxSpeed = 10f;
                kickBall.maxSpin = 0f;
                levelManager.ballsToFire = 3;
                levelManager.bombsToFire = 0;
                break;
            case 2:
                specialMessage = "To easy? Let's krank up the speed!";
                kickBall.startPosition = new Vector3(0f, 0.5f, 0f);
                levelManager.timeToNextShot = 3f;
                kickBall.minSpeed = 10f;
                kickBall.maxSpeed = 15f;
                kickBall.maxSpin = 1f;
                levelManager.ballsToFire = 6;
                levelManager.bombsToFire = 0;
                break;
            case 3:
                specialMessage = "Be careful for the bombs!";
                kickBall.startPosition = new Vector3(0f, 0.5f, 0f);
                levelManager.timeToNextShot = 2f;
                kickBall.minSpeed = 12f;
                kickBall.maxSpeed = 15f;
                kickBall.maxSpin = 3f;
                levelManager.ballsToFire = 5;
                levelManager.bombsToFire = 2;
                break;
            case 4:
                specialMessage = "This is a hard one! Look to your right!";
                kickBall.startPosition = new Vector3(-10f, 0.5f, 0f);
                levelManager.timeToNextShot = 2f;
                kickBall.minSpeed = 8f;
                kickBall.maxSpeed = 20f;
                kickBall.maxSpin = 5f;
                levelManager.ballsToFire = 6;
                levelManager.bombsToFire = 4;
                break;
            case 5:
                specialMessage = "Did you manage? Let's make it more unpredictable >:)";
                kickBall.startPosition = new Vector3(5f, -5f, 0f);
                levelManager.timeToNextShot = 1.5f;
                kickBall.minSpeed = 8f;
                kickBall.maxSpeed = 20f;
                kickBall.maxSpin = 8f;
                levelManager.ballsToFire = 7;
                levelManager.bombsToFire = 7;
                break;
            case 6:
                specialMessage = "CRAZY MODE ACTIVATE*!**!*!";
                kickBall.startPosition = new Vector3(10f, -10f, 2f);
                levelManager.timeToNextShot = 1.2f;
                kickBall.minSpeed = 8f;
                kickBall.maxSpeed = 25f;
                kickBall.maxSpin = 10f;
                levelManager.ballsToFire = 6;
                levelManager.bombsToFire = 10;
                break;
        }
    }
}