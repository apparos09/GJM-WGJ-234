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

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // if entering a collision with a wall.
    protected void OnCollisionEnter(Collision collision)
    {
        // parent collision check.
        base.OnCollisionEnter(collision);

        // stop moving, and change direction.
        moving = false;
        axis *= -1;
    }

    // mouse click.
    public override void OnMouseButtonDown(Mouse3D mouse3D)
    {
        moving = true;
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

    // Update is called once per frame
    void Update()
    {
        // if the tile is moving.
        if(moving)
        {
            // rigidBody.AddForce(direction)
            transform.Translate(direction * axis * speed * Time.deltaTime);
        }
    }
}
