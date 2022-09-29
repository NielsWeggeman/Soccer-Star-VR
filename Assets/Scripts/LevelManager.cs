// *---- Purpose of this file: ----*
// Manage the progress of the level, from starting the level to sending the
// player to the next level and everything in between.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{

    public MenuManager menuManager;

    public LevelData levelData;

    public AudioManager AM;

    #region

    public GameObject StartMessage;
    public GameObject PauseMenu;
    public GameObject EndScreen;

    public GameObject WristMenu;

    public GameObject RRayInteractor;
    public GameObject LRayInteractor;

    public GameObject RPad;
    public GameObject LPad;

    public TextMeshProUGUI level;
    public TextMeshProUGUI amountOfProjectiles;
    public TextMeshProUGUI specialMessage;

    public TextMeshProUGUI endScore;

    public GameObject Projectile;

    #endregion

    public KickBall kickBall;

    public AudioSource sound;
    public AudioSource crowdSource;

    public AudioClip kickSound;
    public AudioClip ballStopped;
    public AudioClip letThrough;
    public AudioClip explosion;
    public AudioClip fuse;
    public AudioClip whistle;

    public TextMeshProUGUI score;
    public TextMeshProUGUI ballsStopped_UI;
    public TextMeshProUGUI ballsLetThrough_UI;
    public TextMeshProUGUI bombsDodged_UI;
    public TextMeshProUGUI bombsTriggered_UI;

    public bool levelStarted = false;
    public bool levelFinished = false;
    public bool leveledUp = false;
    public bool gameOver = false;

    public int ballsToFire = 10;
    public int bombsToFire = 5;

    public float timeToActivate = 0;

    public int ballsFired = 0;
    public int bombsFired = 0;

    public int ballsLetThrough = 0;
    public int bombsDodged = 0;
    public int bombsTriggered = 0;

    private bool projectileReceived = true;
    private bool projectileLetThrough = false;
    private bool projectileRegistered = true;

    private int scoreCalculation = 0;
    private int totalScore = 0;

    public float volumeScale = 1f;

    private float _enterStartTime;

    public float timeToNextShot = 10f;


    void Start()
    {
        // Set first level.
        levelData.level = 1;

        // Start countdown to first ball shot.
        _enterStartTime = Time.time;

        Time.timeScale = 0;


        // Show starting message to start off the level.
        StartMessage.SetActive(true);

        // Hide the pause menu.
        PauseMenu.SetActive(false);
        EndScreen.SetActive(false);

        // Set levelStarted to false.
        levelStarted = false;
    }


    void Update()
    {
        // If the level was not yet started, show the starting message.
        if (!gameOver)
        {
            if (!levelStarted)
            {
                showLevelStart();
                leveledUp = false;
                levelFinished = false;
            }
            else
            {
                StartMessage.SetActive(false);

                // Once the level has been started, keep tabs on whether the
                // player has pressed pause.
                checkForPause();

                // During the level, the game detects how the user has responded
                // to the objects being launched towards the goal.
                detectUserAction();

                // If there are still objects to be launched during this level,
                // the countdown to the next shot starts once the last object has
                // reached the player.
                if ((ballsFired + bombsFired) < (ballsToFire + bombsToFire + 1) && projectileReceived)
                {
                    countDownToNextShot();
                }

                // If instead there are no more objects to be launched,
                // the game lets the player go to the next level.
                if ((ballsFired + bombsFired) >= (ballsToFire + bombsToFire) && !levelFinished)
                {
                    levelFinished = true;
                    totalScore += scoreCalculation;
                    scoreCalculation = 0;
                }

                if (levelData.level > levelData.amountOfLevels)
                {
                    gameOver = true;
                    crowdSource.PlayOneShot(whistle, 0.5f * AM.masterVolume);
                }
            }
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            EndScreen.SetActive(true);
            RRayInteractor.SetActive(true);
            LRayInteractor.SetActive(true);

            WristMenu.SetActive(false);
            RPad.SetActive(false);
            LPad.SetActive(false);

            endScore.text = totalScore + "";
        }
    }

    // The function detectUserAction detects how the user responded to the
    // objects being flung at them.
    void detectUserAction()
    {
        // First, this code block detects whether the object has come
        // close to the goal. If it has not yet been registered in this area,
        // the projectile is registered and the countdown starts.

        // Depending on the objects tag, it gets registered as a ball or a bomb.
        // This affects how the user should respond to the object, and what
        // will happen if the user touches it.

        // For now, the code just counts how many balls and bombs it has
        // launched so far, and resets the settings so that the next object
        // can be launched.

        if (Projectile.transform.position.z > 13 && !projectileReceived)
        {
            _enterStartTime = Time.time;

            if (kickBall.projectileTag == "ball")
            {
                ballsFired++;
            }
            else if (kickBall.projectileTag == "bomb")
            {
                bombsFired++;
            }
            projectileReceived = true;
            projectileRegistered = false;
            kickBall.wasReset = false;
        }

        // IF the object has reached the goal, this is detected and the crowd
        // gives negative feedback by letting hear an "Awwww" sound.
        if (Projectile.transform.position.z > 15 && !projectileLetThrough)
        {
            projectileLetThrough = true;

            if (kickBall.projectileTag == "ball")
            {
                crowdSource.PlayOneShot(letThrough, 2.0f * AM.masterVolume);
            }
        }
    }

    // Once the latest shot has been registered, the points display at the
    // end of the field gets updated to let the user know how well they did.
    void updateUI()
    {
        ballsStopped_UI.text = ballsFired - ballsLetThrough + "";
        ballsLetThrough_UI.text = ballsLetThrough + "";
        bombsDodged_UI.text = bombsDodged + "";
        bombsTriggered_UI.text = bombsTriggered + "";
        scoreCalculation = ((ballsFired - ballsLetThrough) - ballsLetThrough + bombsDodged*2 - bombsTriggered*3)*100;
        score.text = scoreCalculation + totalScore + "";
    }

    // And, after the shot has been registered, the countdown clock is started
    // before the next shot launches. A.o. by changing the timeToNextShot
    // variable, the difficulty of levels can be increased.

    void countDownToNextShot()
    {
        timeToActivate = (_enterStartTime + timeToNextShot) - Time.time;
        var progress = 1 - (timeToActivate / timeToNextShot);
        progress = Mathf.Clamp(progress, 0, 1);

        // Halfway through the countdown, the ball gets set back to the kick-off
        // position, so that the user can prepare for the next shot.

        // This is also the point where is finally registered whether the object
        // was actually stopped or let through. This was a work-around because
        // I could not get collision detection set up properly within this
        // timeframe.

        if (progress >= .5 && !kickBall.wasReset)
        {
            if (projectileLetThrough == true)
            {
                if (kickBall.projectileTag == "ball")
                {
                    ballsLetThrough++;
                } else {
                    bombsDodged++;
                }
            }

            updateUI();
            kickBall.resetProjectile();

            // Make sure LevelUp only gets called once.

            if (levelFinished && !leveledUp)
            {
                levelStarted = false;
                levelData.level++;
                leveledUp = true;

                levelData.levelStarted();

                ballsLetThrough = 0;
                bombsDodged = 0;
                bombsTriggered = 0;
                ballsFired = 0;
                bombsFired = 0;
            }
        }

        // Once the countdown has been completed, the object gets kicked back
        // to the player. A 'kick' sound is played, and the registration
        // variables are set up so that the game can detect again how the user
        // reacted to this shot.

        if (progress == 1)
        {
            kickBall.shootProjectile();
            sound.PlayOneShot(kickSound, 2.5f * AM.masterVolume);
            projectileReceived = false;
            projectileLetThrough = false;
            projectileRegistered = false;
        }
    }

    // If the player presses 'pause' on their wristmenu during game, the
    // game is paused by setting the timeScale to 0, disabling certain objects
    // that might hinder interaction and enabling the necessary interactors.
    void checkForPause()
    {
        if (menuManager.gamePaused)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            WristMenu.SetActive(false);
            RRayInteractor.SetActive(true);
            LRayInteractor.SetActive(true);
            RPad.SetActive(false);
            LPad.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            WristMenu.SetActive(true);
            RRayInteractor.SetActive(false);
            LRayInteractor.SetActive(false);
            RPad.SetActive(true);
            LPad.SetActive(true);
        }
    }

    // If the level has not yet been started, this codeblock, similarly to the
    // pause screen, activates the start message screen and sets the timescale
    // to 0 and hides the wristmenu and hand pads. It is intended to update
    // the level values to the values for the new level.

    void showLevelStart()
    {

        Time.timeScale = 0;

        // Show start message & ray interactors
        StartMessage.SetActive(true);
        RRayInteractor.SetActive(true);
        LRayInteractor.SetActive(true);

        // Set up the Start Message.
        level.text = "Level " + levelData.level;
        var totalAmountOfBalls = ballsToFire + bombsToFire;
        amountOfProjectiles.text = "Amount of balls: " + totalAmountOfBalls;
        specialMessage.text = levelData.specialMessage;

        // Hide unnecessary objects that may hinder interaction
        WristMenu.SetActive(false);
        RPad.SetActive(false);
        LPad.SetActive(false);

        //Update level data settings.
        levelData.levelStarted();

        // Prepare UI for new level.
        updateUI();

        // What happens if the player presses 'Start Level'
        if (levelStarted)
        {
            Time.timeScale = 1;

            _enterStartTime = Time.time;

            kickBall.resetProjectile();

            // Hide Start Message and interactors
            RRayInteractor.SetActive(false);
            LRayInteractor.SetActive(false);

            // Show game elements used for playing the game again
            WristMenu.SetActive(true);
            RPad.SetActive(true);
            LPad.SetActive(true);
        }
    }
}

