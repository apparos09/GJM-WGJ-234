using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the title manager.
public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // toggles a game object 
    public void ToggleGameObjectActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
