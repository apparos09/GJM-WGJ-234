using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the entrance to the puzzle area.
public class PuzzleAreaEntrance : MonoBehaviour
{
    // the area this puzzle entrance belongs to.
    public PuzzleArea area;

    // rail car that's entering the area. Only one car can be locked at a time.
    public RailCar car;

    // the reset position for the car.
    public Vector3 carResetPos;

    // reset for car's start index.
    public int carResetStartIndex = 0;

    // reset for car's end index.
    public int carResetEndIndex = 1;

    // if 'true', the entrance locks cars.
    public bool lockCar = true;

    // if 'true', the locked car is moved to the entrance's position.
    public bool moveLockedCar = true;

    // Start is called before the first frame update
    void Start()
    {
        // not needed since PuzzleArea handles this.
        // // gives entrance to area if not set.
        // if (area != null)
        // {
        //     // area entrance not set.
        //     if (area.areaEntrance == null)
        //         area.areaEntrance = this;
        // }
            
    }

    // called when the entrance is entered.
    public void OnTriggerEnter(Collider other)
    {
        // no car caught.
        if(car == null && lockCar)
        {
            // tries to get the rail car.
            if(other.gameObject.TryGetComponent<RailCar>(out car))
            {
                OnEntranceEnter();
            }    
        }
    }

    // called when leaving the trigger.
    public void OnTriggerExit(Collider other)
    {
        // car is currently saved.
        if(car != null)
        {
            // the car has left the entrance.
            if(other.gameObject == car.gameObject)
            {
                OnEntranceExit();
            }
        }
    }

    // called when the car is entering the entrance.
    private void OnEntranceEnter()
    {
        // stops car from moving.
        car.paused = true;

        // lock car in the entrance's position.
        if (moveLockedCar)
            car.transform.position = transform.position;

        // saves the reset position.
        carResetPos = car.transform.position;
        carResetStartIndex = car.startNodeIndex;
        carResetEndIndex = car.endNodeIndex;

        area.OnPuzzleStart();
    }

    // called when the car is leaving the entrance.
    private void OnEntranceExit()
    {
        car = null;
    }

    // called to unlock the car.
    public void UnlockCar()
    {
        // unpauses the car's movement.
        if (car != null)
        {
            car.paused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
