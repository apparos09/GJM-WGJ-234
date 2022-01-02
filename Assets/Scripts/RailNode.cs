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
    private void OnTriggerStay(Collider other)
    {
        // the rail car.
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

        // currently attached to a rail, so look for an adjoined rail.
        if(rail != null)
        {
            // the rail node.
            RailNode rn;

            // trys to grab the rail node.
            if (other.gameObject.TryGetComponent<RailNode>(out rn))
            {
                // if it's a seperate rail, try to connect them.
                if(rn.rail != null && rn.rail != rail)
                {
                    // connects the rails together.
                    // if this node is the last in the current rail, and the next node is the first in its rail.
                    if(rail.GetLastNode() == this && rn.rail.GetFirstNode() == rn.rail)
                    {
                        rail.ConnectRails(rn.rail, true);
                    }
                    // if this node is the first in the current rail, and the next node is the last in its rail.
                    else if (rail.GetLastNode() == this && rn.rail.GetFirstNode() == rn.rail)
                    {
                        // this would end up triggering twice, so this part was taken out.
                        // if this node is the first in the current rail, and the next node is the last in its rail.
                        rn.rail.ConnectRails(rail, true);
                    }
                }
            }
        }
    }

    // called when exiting a trigger.
    private void OnTriggerExit(Collider other)
    {
        // disconnects rails if the rail is not null.
        if (rail != null)
        {
            // the rail node.
            RailNode rn;

            // trys to grab the rail node.
            if (other.gameObject.TryGetComponent<RailNode>(out rn))
            {
                if (rail.nextRail == rn.rail) // next rail in the line.
                {
                    rail.DisconnectNextRail();
                }
                else if (rail.prevRail == rn.rail) // previous rail in the line.
                {
                    rail.DisconnectPreviousRail();
                }
                
            }
        }
    }

    // sets the rail. If 'replace' is true, it does this even if a rail is saved.
    public void SetRail(Rail rail, bool replace = true)
    {
        if(replace || (!replace && this.rail == null))
        {
            this.rail = rail;

            // if not in the list, add it to the list.
            if (!this.rail.nodes.Contains(this))
                this.rail.nodes.Add(this);
        }
    }

    // removes the node from the rail.
    public void RemoveRail()
    {
        // if the rail is set.
        if(rail != null)
        {
            rail.nodes.Remove(this); // remove from list.
            rail = null;
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
