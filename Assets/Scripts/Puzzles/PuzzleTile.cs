using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// uses for moving tiles around in the play area. Recommended that continous collision detection is used.
public abstract class PuzzleTile : MonoBehaviour
{
    // public Material mat;

    // can be interacted with.
    public bool interactable = true;

    // rigidbody for the tile.
    public Rigidbody rigidBody;

    // the puzzle area this tile belongs to.
    public PuzzleArea puzzleArea;

    // initial transformation.
    private Vector3 initialPos = Vector3.zero;
    private Quaternion initialRot = Quaternion.identity;
    private Vector3 initialScl = Vector3.one;

    // Start is called before the first frame update
    protected void Start()
    {
        // tries to grab component.
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();

        // gets component.
        if (rigidBody == null)
            gameObject.AddComponent<Rigidbody>();

        // no puzzle area saved, so check for component on parent.
        if(puzzleArea == null && transform.parent != null)
        {
            puzzleArea = transform.parent.gameObject.GetComponent<PuzzleArea>();
        }

        // saves the initial values.
        initialPos = transform.position;
        initialRot = transform.rotation;
        initialScl = transform.localScale;
    }

    // called when the puzzle tile is collided with.
    protected void OnCollisionEnter(Collision collision)
    {
        // player has collided with tile, so return to start.
        if(collision.gameObject.tag == "Player")
        {
            puzzleArea.OnPuzzleFail();
        }
    }

    // when the mouse clicks on the tile.
    public abstract void OnMouseButtonDown(Mouse3D mouse3D);

    // when the mouse holds on a title.
    public abstract void OnMouseButtonHeld(Mouse3D mouse3D);

    // when the mouse release a tile.
    public abstract void OnMouseButtonReleased(Mouse3D mouse3D);

    // resets the tile.
    public virtual void ResetTile()
    {
        // reset tile.
        transform.position = initialPos;
        transform.rotation = initialRot;
        transform.localScale = initialScl;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
