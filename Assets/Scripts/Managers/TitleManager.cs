using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// the title manager.
public class TitleManager : MonoBehaviour
{
    // drop down for changing the screen size.
    public Dropdown screenSizeDropdown;

    // the exit button.
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // the screen size dropdown has not been set.
        if (screenSizeDropdown == null)
        {
            // game object.
            GameObject temp = GameObject.Find("Screen Size Dropdown");

            // tries to grab exit button.
            if (temp != null)
                temp.TryGetComponent<Dropdown>(out screenSizeDropdown);
        }

        // sets to current screen size.
        if (screenSizeDropdown != null)
        {
            // if in full-screen, use option 0.
            if (Screen.fullScreen)
            {
                screenSizeDropdown.value = 0;
            }
            else // specifics screen size.
            {
                // checks current screen size.
                int screenY = Screen.height;

                // checks screen size to see default value.
                switch (screenY)
                {
                    case 1080: // big
                        screenSizeDropdown.value = 1;
                        break;

                    case 720: // medium
                        screenSizeDropdown.value = 2;
                        break;

                    case 480: // small
                        screenSizeDropdown.value = 3;
                        break;
                }
            }

        }

        // the exit button has not been set.
        if (exitButton == null)
        {
            // game object.
            GameObject temp = GameObject.Find("Exit Button");

            // tries to grab exit button.
            if (temp != null)
                temp.TryGetComponent<Button>(out exitButton);
        }

        // disables functions in web mode.
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // disable screen size change.
            if (screenSizeDropdown != null)
                screenSizeDropdown.interactable = false;

            // disable exit button.
            if (exitButton != null)
                exitButton.interactable = false;
        }
        
    }

    // called by dropdown.
    public void OnScreenSizeDropdownChange()
    {
        // get screen size from dropdown
        if (screenSizeDropdown != null)
            ChangeScreenSize(screenSizeDropdown.value);
    }

    // called when the screen size changes.
    public void ChangeScreenSize(int option)
    {
        switch (option)
        {
            case 0: // Full Screen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
                break;

            case 1: // 1920 X 1080
                Screen.SetResolution(1920, 1080, FullScreenMode.MaximizedWindow);
                Screen.fullScreen = false;
                break;

            case 2: // 1280 X 720
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                Screen.fullScreen = false;
                break;

            case 3: // 854 X 480 (854 rounded up from 853.333)
                Screen.SetResolution(854, 480, FullScreenMode.Windowed);
                Screen.fullScreen = false;
                break;
        }
    }

    // passes object
    public void EnableGameObject(GameObject go)
    {
        go.SetActive(true);
    }

    // passes object
    public void DisableGameObject(GameObject go)
    {
        go.SetActive(false);
    }

    // toggles a game object 
    public void ToggleGameObjectActive(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    // plays the game.
    public void PlayGame()
    {
        // loads the round scene.
        SceneManager.LoadScene("GameScene");
    }

    // exits the game
    public void ExitApplication()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
