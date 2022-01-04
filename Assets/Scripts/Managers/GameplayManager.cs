using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// the manager for the game.
public class GameplayManager : MonoBehaviour
{
    // the end of the stage.
    public Goal goal;

    // the timer
    public CountdownTimer timer;

    // Start is called before the first frame update
    void Start()
    {
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
            
    }

    // called when the goal is reached.
    public void OnGoalReached()
    {
        OnGameEnd(true);
    }

    // called wehn the game ends.
    public void OnGameEnd(bool won)
    {
        SceneManager.LoadScene("TitleScene");
    }


    // Update is called once per frame
    void Update()
    {
        // time's up.
        if (timer.IsFinished())
            OnGameEnd(false);
    }
}
