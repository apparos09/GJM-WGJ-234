using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle area.
public class PuzzleArea : MonoBehaviour
{
    // the player object.
    public Player player;

    // the entrance into this area.
    public PuzzleAreaEntrance areaEntrance;

    // the exit into this area.
    public PuzzleAreaExit areaExit;

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

    }

    // called when entering the area.
    public void OnPuzzleStart()
    {
        player.EnablePuzzleCamera();

        // if camera settings should be adjusted when the puzzle starts.
        if (adjustCamSettings)
            ChangeCameraSettings();
    }

    // called when the puzzle is passed.
    public void OnPuzzlePass()
    {
        player.EnableCinematicCamera();
    }

    // public void OnPuzzleFail().

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
