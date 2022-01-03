using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player, which is used to click on and interact with items.
public class Player : MonoBehaviour
{
    // the player's rail car.
    public RailCar car;

    // public PuzzleArea area;
    
    // mouse 3D for interacting with objects, and the tile interacted with.
    public Mouse3D mouse3D = null;
    public PuzzleTile clickedTile = null;

    // cameras
    public Camera cineCam; // the cinematic camera.
    public Camera puzzleCam; // the puzzle camera.

    // if 'true', the player is locked in a puzzle.
    public bool inPuzzle;

    // if 'true', the mouse functions are enabled.
    public bool mouseEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        // grabs the player's rail car. There should only be one rail car.
        if (car == null)
            car = FindObjectOfType<RailCar>();

        // finds mouse 3D.
        if (mouse3D == null)
            mouse3D = FindObjectOfType<Mouse3D>();

        // if the cinematic camera does not exist, use the main camera.
        if (cineCam == null)
            cineCam = Camera.main;

        // if the puzzle camera does not exist, use the main camera.
        if (puzzleCam == null)
            puzzleCam = Camera.main;
    }

    // swaps between the cinematic camera and 
    public void SwapCameras()
    {
        // swaps cameras
        if(cineCam.enabled == true) // enable puzzle
        {
            EnablePuzzleCamera();
        }
        else if(puzzleCam.enabled == true) // enable cine
        {
            EnableCinematicCamera();
        }
        else
        {
            Camera.main.enabled = true;
        }
    }

    // enables the cinematic camera.
    public void EnableCinematicCamera()
    {
        puzzleCam.enabled = false;
        cineCam.enabled = true;

        inPuzzle = false;
        mouseEnabled = false;
    }

    // enables the puzzle camera.
    public void EnablePuzzleCamera()
    {
        cineCam.enabled = false;
        puzzleCam.enabled = true;
        
        inPuzzle = true;
        mouseEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if in a puzzle
        if(inPuzzle && mouseEnabled)
        {
            // interacting with puzzle tiles.
            // goes through mouse inputs.
            if (mouse3D.clickedObject != null && clickedTile == null) // mouse down
            {
                // tries to grab component.
                if (mouse3D.clickedObject.TryGetComponent<PuzzleTile>(out clickedTile))
                {
                    clickedTile.OnMouseButtonDown(mouse3D.mouseWorldPosition);
                }
            }
            else if (mouse3D.clickedObject != null && clickedTile != null) // mouse held
            {
                if (mouse3D.clickedObject.gameObject == clickedTile.gameObject)
                    clickedTile.OnMouseButtonHeld(mouse3D.mouseWorldPosition);
            }
            else if (mouse3D.clickedObject == null && clickedTile != null) // mouse released.
            {
                clickedTile.OnMouseButtonReleased(mouse3D.mouseWorldPosition);
                clickedTile = null;
            }
        }

        // temp (moving around the player).
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }


    }
}
