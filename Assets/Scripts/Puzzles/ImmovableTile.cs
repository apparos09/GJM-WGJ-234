using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableTile : PuzzleTile
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // the mouse functions don't do anything.
    // mouse down
    public override void OnMouseButtonDown(Mouse3D mouse3D)
    {
        
    }

    // mosue held
    public override void OnMouseButtonHeld(Mouse3D mouse3D)
    {
        
    }

    // mouse released
    public override void OnMouseButtonReleased(Mouse3D mouse3D)
    {
        
    }

    

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
