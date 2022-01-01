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

    // Start is called before the first frame update
    void Start()
    {
        if (addChildren)
        {
            List<RailNode> temp = new List<RailNode>();
            GetComponentsInChildren<RailNode>(false, temp);
            nodes.AddRange(temp);
        }

        // goes through each node.
        foreach(RailNode node in nodes)
        {
            node.rail = this;
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

    // returns 'true' if the car is at the end o the rail.
    public bool AtEndOfRail(RailCar car)
    {
        // no nodes, so rail does not exist.
        if (nodes.Count == 0)
            return false;

        // gets the last node.
        int lastNodeIndex = nodes.Count - 1;
        RailNode lastNode = nodes[lastNodeIndex];

        bool result = (car.transform.position == lastNode.transform.position &&
            (car.startNodeIndex >= lastNodeIndex));

        return result;
    }

    // runs the interpolation along the rail, returning 'true' if the car moved.
    public bool Run(RailCar car)
    {
        // TODO: adjust it so that it goes at the same speed throughout.

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

        // reached the end of the current link, so move onto the next one.
        if(car.t_value >= 1.0F)
        {
            car.t_value = 0.0F;

            car.startNodeIndex = car.endNodeIndex;
            car.endNodeIndex++;

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
