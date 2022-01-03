using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// uses for moving tiles around in the play area.
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
    public abstract void OnMouseButtonDown();

    // when the mouse holds on a title.
    public abstract void OnMouseButtonHeld();

    // when the mouse release a tile.
    public abstract void OnMouseButtonReleased();

    // Update is called once per frame
    void Update()
    {
        
    }
}
