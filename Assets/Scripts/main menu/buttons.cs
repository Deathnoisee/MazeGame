using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Assign this in the inspector

    // Starts the game (Loads the main scene)
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Change "GameScene" to your actual scene name
    }

    // Opens the options panel
    public void OpenOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    // Closes the options panel
    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    // Exits the game (Works in build, not in editor)
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in editor
        #endif
    }
}
