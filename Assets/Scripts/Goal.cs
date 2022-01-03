using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the end goal of the stage.
public class Goal : MonoBehaviour
{
    // manager for the gameplay
    public GameplayManager manager;

    // Start is called before the first frame update
    void Start()
    {
        // finds the gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();
    }

    // called when something enters the goal zone.
    private void OnTriggerEnter(Collider other)
    {
        // the player spark has collided with the goal.
        if (other.gameObject.tag == "Player")
        {
            // finds manager if it isn't saved.
            if (manager == null)
                manager = FindObjectOfType<GameplayManager>();

            // calls the manager to say that the goal has bene reached.
            if (manager != null)
                manager.OnGoalReached();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
