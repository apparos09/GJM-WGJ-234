using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// has the player grab and translate the tile.
public class TranslateTile : PuzzleTile
{
    // mouse 3D object.
    // private Mouse3D mouse3D;

    // the mouse's world position.
    private Vector3 mouseWorldPos;

    // the max momvement speed of the tile.
    // this didn't stop going through walls, so it's practicall useless.
    public float maxMove = 100.0F;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // tile has collided with something.
    new protected void OnCollisionEnter(Collision collision)
    {
        // parent collision check.
        base.OnCollisionEnter(collision);

        // if (mouse3D != null)
        //     mouse3D.clickedObject = null;
    }

    // mouse click.
    public override void OnMouseButtonDown(Mouse3D mouse3D)
    {
        // this.mouse3D = mouse3D;
        mouseWorldPos = mouse3D.mouseWorldPosition; 
    }

    // mouse held (moves on drag)
    public override void OnMouseButtonHeld(Mouse3D mouse3D)
    {
        // mouse is not in the window, so ignore its commands.
        if (!Mouse3D.MouseInWindow())
            return;
        
        // saves mouse 3D.
        // this.mouse3D = mouse3D;

        // movement
        Vector3 move = mouse3D.mouseWorldPosition - mouseWorldPos;
        move.y = 0.0F;

        // casts ray to see if the tile can move that way.
        Ray ray = new Ray(transform.position, (move - transform.position).normalized);
        RaycastHit hitInfo;

        // cast the ray.
        bool rayHit = Physics.Raycast(ray, out hitInfo, 1.0F);

        // translates the object if there is room.
        if(!rayHit)
        {
            // clamps movement speed.
            if (move.magnitude > maxMove)
                move = Vector3.ClampMagnitude(move, maxMove);

            transform.Translate(move);
        }

        mouseWorldPos = mouse3D.mouseWorldPosition;
    }

    // mouse released.
    public override void OnMouseButtonReleased(Mouse3D mouse3D)
    {
        // mouse3D = null;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
}
