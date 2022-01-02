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

    // if 'true', the rail car goes in reverse.
    public bool reversed = false;

    // reveres once the rail car reaches the end.
    // public bool reverseOnEnd = false;

    // if 'true', the car detaches once it reache the end.
    // 'loop' takes priority over this variable.
    public bool detachOnEnd = false;

    // if 'true', the car shifts through rails that are connected.
    public bool shiftRails = true;

    // a rigidbody for the rail car.
    public Rigidbody rigidBody;

    [Header("Rails")]

    // // if 't' 
    // public bool saveLastRails;

    // the current rail the rail car is on.
    public Rail currRail;

    // the current index
    public int startNodeIndex = 0;

    // the destinaton index.
    public int endNodeIndex = 1;

    // the time
    [Tooltip("Value t, which is the value provided to interpolate along the rail.")]
    public float t_value;

    // multiplier for the t-value incrementation.
    // this should always be positive.
    public float t_mult = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        // rigid body not set.
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();

        // going in reverse, so swap around nodes.
        if(reversed && endNodeIndex > startNodeIndex)
        {
            int temp = endNodeIndex;
            endNodeIndex = startNodeIndex;
            startNodeIndex = temp;
        }
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
        t_value += value * Mathf.Abs(t_mult);

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
            // if at the end.
            bool atEnd = currRail.AtEndOfRail(this);

            // // if the car should reverse at the end.
            // if (atEnd && reverseOnEnd)
            // {
            //     reversed = !reversed;
            //     int temp = endNodeIndex;
            //     endNodeIndex = startNodeIndex;
            //     startNodeIndex = temp;
            // }
                

            // moved here so that it happens if the variable is changed.
            // for looping along the rail
            if (loop && atEnd)
            {
                // checks if at the end of the rail.
                if(!reversed && startNodeIndex >= currRail.nodes.Count - 1) // moving forward
                {
                    endNodeIndex = 0;
                }
                else if (reversed && startNodeIndex <= 0) // moving backwards
                {
                    endNodeIndex = currRail.nodes.Count - 1;
                }
            }
            else if(!loop && atEnd && detachOnEnd) // detach from rail.
            {
                currRail = null;

                // gives rigidbody its gravity back.
                // if more fleshed out, it would need to be known if this was enabled to begin with.
                // however, this should be fine.
                if (rigidBody != null)
                    rigidBody.useGravity = true;

                return;
            }

            currRail.Run(this);
        }
    }
}
