/*
 * Reference:
 *  - https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/
 */
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
        // TODO: check and see if focal length is the best way to go about this.
        if(cam.orthographic) // orthographic (2d camera) - uses near clip plane so that it's guaranteed to be positive.
            return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        else // perspective (3d camera)
            return cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.focalLength));
    }

    // gets the mosut target position in world space using the main camera.
    public Vector3 GetMouseTargetPositionInWorldSpace(GameObject refObject)
    {
        // return GetMouseTargetPositionInWorldSpace(Camera.main, refObject);
        return GetMouseTargetPositionInWorldSpace(Camera.main, refObject.transform.position);
    }

    // gets the mouse target position in world space.
    public Vector3 GetMouseTargetPositionInWorldSpace(Camera cam, GameObject refObject)
    {
        // Vector3 camWPos = GetMousePositionInWorldSpace(cam);
        // Vector3 target = camWPos - refObject.transform.position;
        // return target;

        return GetMouseTargetPositionInWorldSpace(Camera.main, refObject.transform.position);
    }

    // gets the mouse target position in world space with a reference vector.
    public Vector3 GetMouseTargetPositionInWorldSpace(Vector3 refPos)
    {
        return GetMouseTargetPositionInWorldSpace(Camera.main, refPos);
    }

    // gets the mouse target position in world space with a reference vector.
    public Vector3 GetMouseTargetPositionInWorldSpace(Camera cam, Vector3 refPos)
    {
        Vector3 camWPos = GetMousePositionInWorldSpace(cam);
        Vector3 target = camWPos - refPos;
        return target;
    }

    // Update is called once per frame
    void Update()
    {
        // when the left mouse button is clicked.
        // if(Input.GetAxisRaw("Fire1") != 0)
        // if (Input.GetMouseButtonDown(0))
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 target; // ray's target
            Ray ray; // ray object
            RaycastHit hitInfo; // info on hit.
            bool rayHit; // true if the ray hit.

            // checks if the camera is perspective or orthographic.
            if (Camera.main.orthographic) // orthographic
            {
                // gets the mouse position in world space, giving it the camera's near plane.
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane;

                // the mouse world position here will be the camera's position offset by the mouse's position.
                // the z-value will be the camera's z plus the mouse's z-value.
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

                // tries to get a hit. Since it's orthographic, the ray goes straight forward.
                target = Vector3.forward; // target is into the screen.
                ray = new Ray(mouseWorldPos, target.normalized); // ray position is mouse position in world space.
                rayHit = Physics.Raycast(ray, out hitInfo, Camera.main.farClipPlane - Camera.main.nearClipPlane);
            }
            else // perspective.
            {
                target = GetMouseTargetPositionInWorldSpace(Camera.main.gameObject);
                ray = new Ray(Camera.main.transform.position, target.normalized);
                rayHit = Physics.Raycast(ray, out hitInfo);
            }

            // hit.collider.gameObject.tag
            if(rayHit)
                Debug.Log(hitInfo.collider.gameObject.name);

        }

    }
}
