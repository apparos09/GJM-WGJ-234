using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a chain for interpolation.
public class Rail : MonoBehaviour
{
    // if 'true', the rail adds its children from the start.
    public bool addChildren = true;

    // the list of nodes.
    public List<RailNode> nodes = new List<RailNode>();

    // the next rail, which is locked to the end of the rail.
    public Rail nextRail;

    // the previous rail, which is locked to the start of the rail.
    public Rail prevRail;

    // Start is called before the first frame update
    void Start()
    {
        // adds the children.
        if (addChildren)
        {
            List<RailNode> temp = new List<RailNode>();
            GetComponentsInChildren<RailNode>(false, temp);
            nodes.AddRange(temp);
        }

        // goes through each node.
        foreach(RailNode node in nodes)
        {
            node.SetRail(this);
        }
    }

    // returns the index of the node. If the node isn't in the list, it returns a -1.
    public int GetNodeIndex(RailNode node)
    {
        // goes through each index
        for(int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] == node)
                return i;
        }

        return -1;
    }

    // gets the first node.
    public RailNode GetFirstNode()
    {
        if (nodes.Count > 0)
            return nodes[0];
        else
            return null;
    }

    // gets the last node.
    public RailNode GetLastNode()
    {
        if (nodes.Count > 0)
            return nodes[nodes.Count - 1];
        else
            return null;
    }

    // returns 'true' if the car is at the end o the rail.
    public bool AtEndOfRail(RailCar car)
    {
        // no nodes, so rail does not exist.
        if (nodes.Count == 0)
            return false;

        // gets the last node.
        int lastNodeIndex = (car.reversed) ? 0 : nodes.Count - 1;
        RailNode lastNode = nodes[lastNodeIndex];

        bool result = (car.transform.position == lastNode.transform.position &&
            (car.startNodeIndex >= lastNodeIndex));

        return result;
    }

    // connects two rails. This makes r2 come after rail 1.
    // if 'disconnect' is set to 'true', then it overrides existing rail connections.
    public void ConnectRails(Rail r2, bool disconnect)
    {
        nextRail = r2;
        r2.prevRail = this;
    }

    // has car shift rails. Direction is based on index values.
    // if prediction doesn't work, then it moves forward. Returns true if successful.
    public bool ShiftRails(RailCar car)
    {
        if (car.startNodeIndex < car.endNodeIndex) // move forward
            return ShiftRails(car, true);
        else if (car.startNodeIndex > car.endNodeIndex) // move backward
            return ShiftRails(car, false);
        else
            return ShiftRails(car, true);
    }

    // if the car should shift rails. If 'forward' is 'true', then move forward.
    // if 'forward' is false, then the car moves backwards.
    public bool ShiftRails(RailCar car, bool forward)
    {
        if(forward) // move forward
        {
            // next rail does not exist.
            if (nextRail == null)
                return false;

            car.currRail = nextRail;
            car.startNodeIndex = 0;
            car.endNodeIndex = car.startNodeIndex + 1;

            return true;
        }
        else // move backward
        {
            // next rail does not exist.
            if (prevRail == null)
                return false;

            car.currRail = prevRail;
            car.startNodeIndex = prevRail.nodes.Count - 1;
            car.endNodeIndex = car.startNodeIndex - 1;

            return true;
        }
    }

    // runs the interpolation along the rail, returning 'true' if the car moved.
    public bool Run(RailCar car)
    {
        // TODO: adjust it so that it goes at the same speed throughout.
        // TODO: make the reverse happen mid movement instead of waiting until the end.

        // nodes
        RailNode n1 = null, n2 = null;
        int currNode = car.startNodeIndex;
        int destNode = car.endNodeIndex;


        // grabs the current node.
        if(currNode >= 0 && currNode < nodes.Count)
            n1 = nodes[currNode];

        // grab the destination node.
        if (destNode >= 0 && destNode < nodes.Count)
            n2 = nodes[destNode];

        // nodes are not set.
        if (n1 == null || n2 == null)
            return false;

        // increments the t-value with delta time.
        Vector3 nextPos = Vector3.Lerp(n1.transform.position, n2.transform.position, car.IncrementT(Time.deltaTime));
        car.transform.position = nextPos;

        // reached the end of the current chain, so move onto the next one.
        if(car.t_value >= 1.0F)
        {
            car.t_value = 0.0F;
            car.startNodeIndex = car.endNodeIndex;

            if (!car.reversed) // going forward
            {
                car.endNodeIndex++;
            }
            else if(car.reversed) // going backwards
            {
                car.endNodeIndex--;
            }


            // if the car should shift rails.
            if(car.shiftRails)
            {

            }


            // // if the car should loop along the rail.
            // if (car.loop && AtEndOfRail(car))
            //     car.destNodeIndex = 0;

            return true;
        }

        return true;
    }

    // public int Run(RailCar car, int currNode, int destNode)

    // Update is called once per frame
    void Update()
    {
        
    }
}
