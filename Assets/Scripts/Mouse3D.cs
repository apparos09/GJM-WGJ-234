using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for getting the mouse position.
public class Mouse3D : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // gets the mouse position in world space using the main camera.
    public Vector3 GetMousePositionInWorldSpace()
    {
        return GetMousePositionInWorldSpace(Camera.main);
    }
    
    // gets the mouse position in world space.
    public Vector3 GetMousePositionInWorldSpace(Camera cam)
    {
        return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.focalLength));
    }

    // gets the mosut target position in world space using the main camera.
    public Vector3 GetMouseTargetPositionInWorldSpace(GameObject refObject)
    {
        return GetMouseTargetPositionInWorldSpace(Camera.main, refObject);
    }

    // gets the mouse target position in world space.
    public Vector3 GetMouseTargetPositionInWorldSpace(Camera cam, GameObject refObject)
    {
        Vector3 camWPos = GetMousePositionInWorldSpace(cam);
        Vector3 target = camWPos - refObject.transform.position;
        return target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
