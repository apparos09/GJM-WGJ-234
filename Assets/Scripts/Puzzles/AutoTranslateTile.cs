using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a puzzle tile that slides on its own. 
public class AutoTranslateTile : PuzzleTile
{
    // the tile's rigidbody.
    public Rigidbody rigidBody;

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
        // tries to grab component.
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();

        // gets component.
        if (rigidBody == null)
            gameObject.AddComponent<Rigidbody>();
    }

    // mouse click.
    public override void OnMouseButtonDown()
    {
        moving = true;
        // Debug.Log("Test");
    }

    // mouse held.
    public override void OnMouseButtonHeld()
    {
        // throw new System.NotImplementedException();
    }

    // mouse released.
    public override void OnMouseButtonReleased()
    {
        // throw new System.NotImplementedException();
    }

    // if entering a collision with a wall.
    private void OnCollisionEnter(Collision collision)
    {
        // stop moving, and change direction.
        moving = false;
        axis *= -1;
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
