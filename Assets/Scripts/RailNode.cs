using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a node part of a rail.
public class RailNode : MonoBehaviour
{
    // the rail the node is connected to.
    public Rail rail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // called when an object triggers this node.
    private void OnTriggerEnter(Collider other)
    {
        // the rail car
        RailCar rc;

        // trys to grab the rail car.
        if(other.gameObject.TryGetComponent<RailCar>(out rc))
        {
            if(rc.currRail == null)
            {
                rc.endNodeIndex = GetNodeIndex();
                rc.startNodeIndex = rc.endNodeIndex - 1;
                rc.AddRail(rail);
            }
        }
    }

    // gets the rail.
    public Rail GetRail()
    {
        return rail;
    }

    // returns the index of the node.
    public int GetNodeIndex()
    {
        if (rail != null)
            return rail.GetNodeIndex(this);
        else
            return -1;
    }
}
