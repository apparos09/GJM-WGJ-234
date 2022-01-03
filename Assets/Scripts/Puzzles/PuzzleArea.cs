using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle area.
public class PuzzleArea : MonoBehaviour
{
    // the player object.
    public Player player;

    // the puzzle
    [Header("Puzzle")]

    // the entrance into this area.
    public PuzzleAreaEntrance areaEntrance;

    // the exit into this area.
    public PuzzleAreaExit areaExit;

    // the gate for entering the puzzle.
    public GameObject enterGate;

    // the gate for exiting the puzzle.
    public GameObject exitGate;

    // camera details
    [Header("Camera")]

    // the game object used for the camera location.
    // the camera is always facing directly down.
    public GameObject camPosBlock;

    // if 'true', camera settings are adjusted by the area.
    public bool adjustCamSettings = true;

    // the camera's orthographic size.
    public int camOrthoSize = 7;

    // the camera's field of view.
    public int camFieldOfView = 80;

    // Start is called before the first frame update
    void Start()
    {
        // player not set.
        if(player == null)
            player = FindObjectOfType<Player>();

        // looks for entrance in children.
        if (areaEntrance == null)
            areaEntrance = GetComponentInChildren<PuzzleAreaEntrance>();

        // if the area entrance is set.
        if (areaEntrance != null)
            areaEntrance.area = this;

        // looks for exit in children.
        if (areaExit == null)
            areaExit = GetComponentInChildren<PuzzleAreaExit>();

        // if the area exit is set.
        if (areaExit != null)
            areaExit.area = this;

        // entrance gate not set, so try to find it.
        if (enterGate == null)
        {
            Transform temp = transform.Find("Enter Gate");

            if (temp == null)
                temp = transform.Find("Entrance Gate");

            if (temp != null)
                enterGate = temp.gameObject;
        }

        // exit gate not set, so try to find it.
        if(exitGate == null)
        {
            Transform temp = transform.Find("Exit Gate");

            if (temp == null)
                temp = transform.Find("Exiting Gate");

            if(temp != null)
                exitGate = temp.gameObject;
        }

    }

    // called when entering the area.
    public void OnPuzzleStart()
    {
        // enables the puzzle camera.
        player.EnablePuzzleCamera();

        // if camera settings should be adjusted when the puzzle starts.
        if (adjustCamSettings)
            ChangeCameraSettings();

        // activate the gates.
        if (enterGate != null)
            enterGate.SetActive(true);

        if (exitGate != null)
            exitGate.SetActive(true);
    }

    // called when the player tries to the puzzle.
    // puzzle pieces cannot moved at this state.
    public void OnPuzzleTry()
    {
        // deactivate the gates.
        if (enterGate != null)
            enterGate.SetActive(false);

        if (exitGate != null)
            exitGate.SetActive(false);

        // trying the puzzle, so mouse options are disabled.
        player.mouseEnabled = false;
    }

    // called when the puzzle is passed.
    public void OnPuzzlePass()
    {
        player.EnableCinematicCamera();
    }

    // called when the player fails the puzzle.
    // this returns the puzzle back to the start.
    public void OnPuzzleFail()
    {
        // reactivate the gates.
        if (enterGate != null)
            enterGate.SetActive(true);

        if (exitGate != null)
            exitGate.SetActive(true);

        // resets the car position.
        player.car.transform.position = areaEntrance.carResetPos;

        // mouse is enabled again.
        player.mouseEnabled = true;
    }

    public void ChangeCameraSettings()
    {
        Camera.main.orthographicSize = camOrthoSize;
        Camera.main.fieldOfView = camFieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
