using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes
using UnityEngine.UI; // For interacting with buttons

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Assign this in the inspector
    public GameObject[] menuButtons; // Array to hold the buttons that will disappear/reappear

    // Starts the game (Loads the main scene)
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Change "GameScene" to your actual scene name
        BGMManager.instance.StartMusic();
    }

    // Opens the options panel
    public void OpenOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }

        // Deactivate menu buttons when options are open
        ToggleMenuButtons(false);
    }

    // Closes the options panel
    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }

        // Activate menu buttons when options are closed
        ToggleMenuButtons(true);
    }

    // Exits the game (Works in build, not in editor)
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in editor
        #endif
    }

    // Function to activate/deactivate menu buttons
    private void ToggleMenuButtons(bool state)
    {
        foreach (GameObject button in menuButtons)
        {
            button.SetActive(state);
        }
    }
    public void QuitToMainMenu()
    {
        // Stop the background music
        if (BGMManager.instance != null)
        {
            BGMManager.instance.StopMusic();
        }

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu"); // Replace with your actual main menu scene name
    }

}
