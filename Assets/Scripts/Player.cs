using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player, which is used to click on and interact with items.
public class Player : MonoBehaviour
{
    // the player's rail car.
    public RailCar car;

    // mouse 3D for interacting with objects, and the tile interacted with.
    public Mouse3D mouse3D = null;
    public PuzzleTile clickedTile = null;

    // the cinematic camera.
    public Camera cineCam;

    // the puzzle camera.
    public Camera puzzleCam;

    // Start is called before the first frame update
    void Start()
    {
        // grabs the player's rail car. There should only be one rail car.
        if (car == null)
            car = FindObjectOfType<RailCar>();

        // finds mouse 3D.
        if (mouse3D == null)
            mouse3D = FindObjectOfType<Mouse3D>();
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
    }

    // enables the puzzle camera.
    public void EnablePuzzleCamera()
    {
        cineCam.enabled = false;
        puzzleCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // goes through mouse inputs.
        if(mouse3D.clickedObject != null && clickedTile == null) // mouse down
        {
            // tries to grab component.
            if(mouse3D.clickedObject.TryGetComponent<PuzzleTile>(out clickedTile))
            {
                clickedTile.OnMouseButtonDown(mouse3D.mouseWorldPosition);
            }
        }
        else if(mouse3D.clickedObject != null && clickedTile != null) // mouse held
        {
            if(mouse3D.clickedObject.gameObject == clickedTile.gameObject)
                clickedTile.OnMouseButtonHeld(mouse3D.mouseWorldPosition);
        }
        else if(mouse3D.clickedObject == null && clickedTile != null) // mouse released.
        {
            clickedTile.OnMouseButtonReleased(mouse3D.mouseWorldPosition);
            clickedTile = null;
        }
    }
}
