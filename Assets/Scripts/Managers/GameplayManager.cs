using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the manager for the game.
public class GameplayManager : MonoBehaviour
{
    // the end of the stage.
    public Goal goal;

    // Start is called before the first frame update
    void Start()
    {
        // finds goal.
        if (goal == null)
            goal = FindObjectOfType<Goal>();
    }

    // called when the goal is reached.
    public void OnGoalReach()
    {

    }

    // called wehn the game ends.
    public void OnGameEnd(bool won)
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
