using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a car that's going along a rail. This is just an object that goes along a rail.
public class RailCar : MonoBehaviour
{
    // if 'true', the rail car won't move.
    public bool paused = false;

    // if 'true', the rail car loops around the track.
    public bool loop = false;

    // a rigidbody for the rail car.
    public Rigidbody rigidBody;

    [Header("Rails")]

    // // if 't' 
    // public bool saveLastRails;

    // the next rails that will be travelled along.
    public Queue<Rail> nextRails;

    // the current rail the rail car is on.
    public Rail currRail;

    // the rails already passed.
    public Stack<Rail> lastRails;

    // the current index
    public int startNodeIndex = 0;

    // the destinaton index.
    public int endNodeIndex = 1;

    // the time
    [Tooltip("Value t, which is the value provided to interpolate along the rail.")]
    public float t_value;

    // multiplier for the t-value incrementation.
    public float t_mult = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        // rigid body not set.
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
    }

    // adds a rail for the car to go along.
    public void AddRail(Rail rail)
    {
        // adds a rail.
        if (currRail == null)
            currRail = rail;

        // if at the start of the rail.
        if (startNodeIndex == -1)
        {
            // if the rail car should loop.
            if (loop) // start from the end
            {
                startNodeIndex = rail.nodes.Count - 1;
            }
            else // go to the next node.
            {
                startNodeIndex = endNodeIndex;
                endNodeIndex++;
            }

        }

        // if a rigidbody is attached, disable its gravity and remove its velocity.
        if(rigidBody != null)
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.useGravity = false;
        }    
    }

    // increments the t-value using delta time.
    public float IncrementT()
    {
        return IncrementT(Time.deltaTime);
    }

    // increments the t-value using the provided value, multiplying it by t_mult.
    // if 'clamp01' is true, the t-value is clamped between the values of (0 and 1).
    public float IncrementT(float value, bool clamp01 = true)
    {
        // increments the t-value.
        t_value += value * t_mult;

        // clamps value.
        if (clamp01)
            t_value = Mathf.Clamp01(t_value);

        return t_value;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: implement rail changing.

        // runs the update.
        if(!paused && currRail != null)
        {
            // moved here so that it happens if the variable is changed.
            if (loop && currRail.AtEndOfRail(this) && 
                startNodeIndex >= currRail.nodes.Count - 1)
                endNodeIndex = 0;

            currRail.Run(this);
        }
    }
}
