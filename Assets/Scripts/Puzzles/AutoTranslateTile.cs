using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a puzzle tile that slides on its own. 
public class AutoTranslateTile : PuzzleTile
{
    // speed of tile's movement.
    public float speed = 5;

    // direction of movement.
    public Vector3 direction = Vector3.right;

    // axis (1 = positive, -1 = negative).
    public int axis = 1;

    // movement.
    public bool moving = false;

    // uses for resettng.
    private Vector3 initialDirec;
    private int initAxis;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        initialDirec = direction;
        initAxis = axis;
    }

    // if entering a collision with a wall.
    protected new void OnCollisionEnter(Collision collision)
    {
        // parent collision check.
        base.OnCollisionEnter(collision);

        // stop moving, and change direction.
        if (!(collision.gameObject.tag == "Floor" && direction.y != 0))
        {
            moving = false;
            axis *= -1;
        } 
    }

    // mouse click.
    public override void OnMouseButtonDown(Mouse3D mouse3D)
    {
        // moving already set to true, so just flip axis.
        if(moving == true)
        {
            axis *= -1;
        }
        else
        {
            moving = true;
        }
        
        // Debug.Log("Test");
    }

    // mouse held.
    public override void OnMouseButtonHeld(Mouse3D mouse3D)
    {
        // throw new System.NotImplementedException();
    }

    // mouse released.
    public override void OnMouseButtonReleased(Mouse3D mouse3D)
    {
        // throw new System.NotImplementedException();
    }

    // resets the tile.
    public override void ResetTile()
    {
        base.ResetTile();

        moving = false;
        direction = initialDirec;
        axis = initAxis;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        // if the tile is moving.
        if (moving)
        {
            // rigidBody.AddForce(direction)
            // return to default orientation before applying translation.

            // reset rotation.
            Quaternion temp = transform.rotation;
            transform.rotation = Quaternion.identity;

            // move
            Vector3 move = direction * axis * speed * Time.deltaTime;
            transform.Translate(move);

            // return to default orientation.
            transform.Translate(move);
            transform.rotation = temp;

            
        }
    }
}
