using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle area.
// NOTE: MAKE SURE THE WALLS ARE BIG ENOUGH THAT TILES THAT DON'T GO THROUGH.
public class PuzzleArea : MonoBehaviour
{
    // if 'true', an attempt is being made to go through the stage.
    public bool inAttempt = false;

    // the player object.
    public Player player;

    // the gameplay manager.
    public GameplayManager manager;

    // the puzzle
    [Header("Puzzle")]

    // the entrance into this area.
    public PuzzleAreaEntrance areaEntrance;

    // the exit into this area.
    public PuzzleAreaExit areaExit;

    // the gate for entering the puzzle.
    public PuzzleGate enterGate;

    // the gate for exiting the puzzle.
    public PuzzleGate exitGate;

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

        // manager not set.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

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
        if (enterGate == null || exitGate == null)
        {
            PuzzleGate[] gates = GetComponentsInChildren<PuzzleGate>();

            // gates found.
            if(gates.Length > 0)
            {
                // gets first gate.
                if (enterGate == null)
                    enterGate = gates[0];

                // gets last gate.
                if (exitGate == null)
                    exitGate = gates[gates.Length - 1];
            }
        }
    }

    // called when entering the area.
    public void OnPuzzleStart()
    {
        // puzzle has started.
        inAttempt = false;

        // enables the puzzle camera.
        player.EnablePuzzleCamera();

        // gives manager the current puzzle.
        manager.currentPuzzle = this;

        // camera positioning block.
        if (camPosBlock != null)
        {
            player.puzzleCam.transform.position = camPosBlock.transform.position;
        }

        // if camera settings should be adjusted when the puzzle starts.
        if (adjustCamSettings)
            ChangeCameraSettings();

        // activate the gates.
        if (enterGate != null)
            enterGate.gameObject.SetActive(true);

        if (exitGate != null)
            exitGate.gameObject.SetActive(true);
    }

    // called when the player tries to the puzzle.
    // puzzle pieces cannot moved at this state.
    public void OnPuzzleTry()
    {
        // if the player is going through a puzzle attempt.
        if (inAttempt)
        {
            Debug.Log("Currently trying the puzzle.");
            return;
        }

        // currently being tried.
        inAttempt = true;

        // deactivate the gates.
        if (enterGate != null)
            enterGate.gameObject.SetActive(false);

        if (exitGate != null)
            exitGate.gameObject.SetActive(false);

        // trying the puzzle, so car should move, and mouse options are disabled.
        player.car.paused = false;
        player.mouseEnabled = false;
    }

    // called when the puzzle is passed.
    public void OnPuzzlePass()
    {
        inAttempt = false;
        player.EnableCinematicCamera();

        // removes the current puzzle.
        manager.currentPuzzle = null;
    }

    // called when the player fails the puzzle.
    // this returns the puzzle back to the start.
    public void OnPuzzleFail()
    {
        // reset puzzle
        inAttempt = false;

        // reactivate the gates.
        if (enterGate != null)
            enterGate.gameObject.SetActive(true);

        if (exitGate != null)
            exitGate.gameObject.SetActive(true);

        // resets the car position, its indexes, its time, and pauses its movement.
        player.car.transform.position = areaEntrance.carResetPos;
        player.car.startNodeIndex = areaEntrance.carResetStartIndex;
        player.car.endNodeIndex = areaEntrance.carResetEndIndex;
        player.car.t_value = 0;
        player.car.paused = true;

        // mouse is enabled again.
        player.mouseEnabled = true;
    }

    // TODO: add reset function

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
