using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the player, which is used to click on and interact with items.
public class Player : MonoBehaviour
{
    // mouse 3D for interacting with objects, and the tile interacted with.
    public Mouse3D mouse3D = null;
    public PuzzleTile clickedTile = null;

    // Start is called before the first frame update
    void Start()
    {
        // finds mouse 3D.
        if (mouse3D == null)
            mouse3D = FindObjectOfType<Mouse3D>();
    }

    // Update is called once per frame
    void Update()
    {
        // goes through mouse inputs.
        if(mouse3D.clickedObject != null && clickedTile == null) // mouse down
        {
            // tries to grab component.
            if(mouse3D.clickedObject.TryGetComponent<PuzzleTile>(out clickedTile))
            {
                clickedTile.OnMouseButtonDown();
            }
        }
        else if(mouse3D.clickedObject != null && clickedTile != null) // mouse held
        {
            if(mouse3D.clickedObject.gameObject == clickedTile.gameObject)
                clickedTile.OnMouseButtonHeld();
        }
        else if(mouse3D.clickedObject == null && clickedTile != null) // mouse released.
        {
            clickedTile.OnMouseButtonReleased();
            clickedTile = null;
        }
    }
}
