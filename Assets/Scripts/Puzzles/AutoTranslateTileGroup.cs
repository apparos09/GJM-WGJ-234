using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a puzzle tile that slides on its own. 
public class AutoTranslateTileGroup : PuzzleTile
{
    // speed of tile's movement.
    public float speed = 5;

    // direction of movement.
    public Vector3 direction = Vector3.right;

    // axis (1 = positive, -1 = negative).
    public int axis = 1;

    // movement.
    public bool moving = false;

    // group of tiles to also translate.
    public List<AutoTranslateTileGroup> group = new List<AutoTranslateTileGroup>();

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        // makes sure all tiles are in all groups.
        foreach(AutoTranslateTileGroup tile in group)
        {
            if (!tile.group.Contains(this))
                tile.group.Add(this);
        }
    }

    // if entering a collision with a wall.
    protected new void OnCollisionEnter(Collision collision)
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

        // set all the other tiles to move.
        foreach (AutoTranslateTileGroup tile in group)
            tile.moving = true;
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
    new void Update()
    {
        base.Update();

        // if the tile is moving.
        if (moving)
        {
            transform.Translate(direction * axis * speed * Time.deltaTime);
        }
    }
}
