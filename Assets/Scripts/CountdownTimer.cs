using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the countdown timer for the game.
public class CountdownTimer : MonoBehaviour
{
    // the start of the timer.
    public float timerStart = 100.0F;

    // the current timer.
    public float currentTime = 0.0F;

    // if 'true', the timer activates on start.
    public bool activateOnStart = true;

    // timer runs when 'paused' is false.
    public bool paused = false;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = Mathf.Abs(timerStart);

        // activates the timer.
        if (activateOnStart)
        {
            paused = false;
        }
        else
        {
            paused = true;
        }
            
    }

    // sets the current time.
    float CurrentTime
    {
        get
        {
            return currentTime;
        }

        set
        {
            currentTime = Mathf.Abs(value);
        }
    }

    // sets the maximum time.
    float TimerStart
    {
        get
        {
            return timerStart;
        }

        set
        {
            timerStart = Mathf.Abs(value);
        }
    }

    // starts the timer, resetting it to the timer start.
    public void StartTimer()
    {
        currentTime = Mathf.Abs(timerStart);
        paused = false;
    }

    // restarts the timer.
    public void RestartTimer(bool play)
    {
        currentTime = Mathf.Abs(timerStart);
        paused = !play;
    }

    // pauses the timer.
    public void PauseTimer()
    {
        paused = true;
    }

    // unpauses the timer.
    public void UnpauseTimer()
    {
        paused = false;
    }

    // toggles the timer pause.
    public void ToggleTimerPause()
    {
        paused = !paused;
    }

    // checks if the timer is pasued.
    public bool IsPaused()
    {
        return paused;
    }

    // checks if the timer is finished.
    public bool IsFinished()
    {
        if (currentTime <= 0.0F)
            return true;
        else
            return false;

    }

    // Update is called once per frame
    void Update()
    {
        // runs timer.
        if(!paused)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0.0F)
                currentTime = 0.0F;
        }
    }
}
