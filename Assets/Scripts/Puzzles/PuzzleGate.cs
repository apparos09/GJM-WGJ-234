using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for a puzzle gate. This script may not be needed.
// this is just for testing collision with the gate.
// this is only a puzzle tile so that it can be interacted with.
public class PuzzleGate : PuzzleTile
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // // called when something collides with a puzzle gate.
    // protected void OnCollisionEnter(Collision collision)
    // {
    //     base.OnCollisionEnter(collision);
    // 
    //     // if (collision.gameObject.tag == "Player")
    //     //     Debug.Log("Player!");
    // }

    // mouse down
    public override void OnMouseButtonDown(Mouse3D mouse3D)
    {
        // try to pass the puzzle.
        puzzleArea.OnPuzzleTry();
    }

    // mouse held
    public override void OnMouseButtonHeld(Mouse3D mouse3D)
    {
    }

    // mouse released
    public override void OnMouseButtonReleased(Mouse3D mouse3D)
    {
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
