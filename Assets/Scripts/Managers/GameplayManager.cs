using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void OnGoalReached()
    {
        // OnGameEnd();
    }

    // called wehn the game ends.
    public void OnGameEnd()
    {
        SceneManager.LoadScene("TitleScene");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
