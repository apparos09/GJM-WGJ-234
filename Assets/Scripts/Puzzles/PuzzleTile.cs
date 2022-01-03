using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// uses for moving tiles around in the play area. Recommended that continous collision detection is used.
public abstract class PuzzleTile : MonoBehaviour
{
    // public Material mat;

    // can be interacted with.
    public bool interactable = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // public virtual void OnMouseHover();

    // when the mouse clicks on the tile.
    public abstract void OnMouseButtonDown(Vector3 mouseWPos);

    // when the mouse holds on a title.
    public abstract void OnMouseButtonHeld(Vector3 mouseWPos);

    // when the mouse release a tile.
    public abstract void OnMouseButtonReleased(Vector3 mouseWPos);

    // Update is called once per frame
    void Update()
    {
        
    }
}
