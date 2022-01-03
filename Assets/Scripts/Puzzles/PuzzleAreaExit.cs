using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the exit from the puzzle area.
public class PuzzleAreaExit : MonoBehaviour
{
    // the area this puzzle entrance belongs to.
    public PuzzleArea area;

    // rail car that's leaving the area.
    public RailCar car;

    // Start is called before the first frame update
    void Start()
    {
        // not needed this puzzle area handles this.
        // // gives exit.
        // if (area.areaExit != null)
        //     area.areaExit = this;
    }

    // called when the exit is triggered by an entering car.
    private void OnTriggerEnter(Collider other)
    {
        // no car caught.
        if (car == null)
        {
            // tries to get the rail car.
            if (other.gameObject.TryGetComponent<RailCar>(out car))
            {
                OnExitEnter();
            }
        }
    }

    // called when the exit is triggered by a leaving car.
    private void OnTriggerExit(Collider other)
    {
        // car set.
        if (car != null)
        {
            // car exiting.
            if (other.gameObject == car.gameObject)
                OnExitExit();
        }
    }

    // called when entering the exit area.
    public void OnExitEnter()
    {
    }

    // // called when leaving the exit area.
    public void OnExitExit()
    {
        car = null;

        area.OnPuzzlePass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
