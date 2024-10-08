using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// the manager for the game.
public class GameplayManager : MonoBehaviour
{
    // player
    public Player player;

    // the end of the stage.
    public Goal goal;

    // the current puzzle.
    public PuzzleArea currentPuzzle = null;

    // if 'true', the game is paused.
    public bool paused = false;

    // time scale for delta time
    private float baseTimeScale;

    // if set to 'true', a time over results in the game ending.
    public bool timeGameOver = true;
    [Header("Interface")]

    // the timer
    public CountdownTimer timer;

    // the text for the timer.
    public Text timerText;

    // the progress slider to show player progress.
    public Slider progessSlider;

    // the starting distance between the player and the goal.
    public float startDist;

    [Header("Interface/Results")]

    // user interface elements for the UI.
    public bool enabledResults = true;
    
    // the results user interface
    public GameObject resultsUI;
    
    // the win user interface
    public GameObject winUI;
    
    // the lose user interface
    public GameObject loseUI;

    // the results time text
    public Text resultsTimeText;

    // Start is called before the first frame update
    void Start()
    {
        // finds the player.
        if (player == null)
            player = FindObjectOfType<Player>();

        // finds goal.
        if (goal == null)
            goal = FindObjectOfType<Goal>();

        // finds the countdown timer.
        if (timer == null)
            timer = FindObjectOfType<CountdownTimer>();

        // if timer has not been set, add the component.
        if (timer == null)
        {
            timer = gameObject.AddComponent<CountdownTimer>();
        }

        // finds slider if not set.
        progessSlider = FindObjectOfType<Slider>();

        // grabs the starting distance from the goal.
        startDist = (goal.transform.position - player.transform.position).magnitude;

        // saves the base time scale.
        Time.timeScale = 1.0F; // Normal time.
        baseTimeScale = Time.timeScale;

        // hides the results user interface.
        if (resultsUI != null)
            resultsUI.SetActive(false);
    }

    // pause the game.
    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0.0F;
    }

    // unpause the game.
    public void UnpauseGame()
    {
        paused = false;
        Time.timeScale = baseTimeScale;
    }

    // toggle the game's pause function.
    public void ToggleGamePause()
    {
        // calls a function based on what the new state will be.
        if (!paused)
            PauseGame();
        else
            UnpauseGame();
    }

    // called when the goal is reached.
    public void OnGoalReached()
    {
        // TODO: add in animation if time allows.

        OnGameEnd(true);

    }

    // called wehn the game ends.
    public void OnGameEnd(bool won)
    {
        // pauses the timer since the game is over.
        if (timer != null)
            timer.PauseTimer();

        // results UI available.
        if (resultsUI != null && enabledResults)
        {
            resultsUI.SetActive(true);

            // results interface assets available.
            if(winUI != null && loseUI != null)
            {
                if (won)
                {
                    loseUI.SetActive(false);
                    winUI.SetActive(true);
                }
                else
                {
                    winUI.SetActive(false);
                    loseUI.SetActive(true);
                }
            }

            // updates the result time.
            if (resultsTimeText != null)
                resultsTimeText.text = (timer.timerStart - timer.currentTime).ToString("F3");

        } // no results UI. Return to menu.
        else
        {
            ReturnToMenu();
        }
    }

    // return to the main menu.

    public void ReturnToMenu()
    {
        Time.timeScale = 1.0F; // Normal time.
        SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        // update text
        if (timerText != null)
            timerText.text = timer.currentTime.ToString("F3");

        // update slider
        if (progessSlider != null)
        {
            float currDist = (goal.transform.position - player.transform.position).magnitude;
            currDist = currDist / startDist;
            progessSlider.value = Mathf.Clamp01(currDist);
        }
            

        // time's up.
        if (timer.IsFinished() && timeGameOver)
            OnGameEnd(false);
    }
}
