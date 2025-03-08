using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public enum GameOverType
    {
        ZombieTouch, // Player touched by a zombie
        TrapFall,    // Player fell into a trap
        WinGame      // Player opened the ending gate and won
    }

    public GameObject zombieTouchUI;
    public GameObject trapFallUI;
    public GameObject winGameUI;

    // Method to handle game over
    public void gg(GameOverType gameOverType)
    {
        switch (gameOverType)
        {
            case GameOverType.ZombieTouch:
                Debug.Log("Game Over: You were killed by a zombie!");
                zombieTouchUI.SetActive(true);
                break;

            case GameOverType.TrapFall:
                Debug.Log("Game Over: You fell into a trap!");
                trapFallUI.SetActive(true);
                break;

            case GameOverType.WinGame:
                Debug.Log("You Win: You escaped!");
                winGameUI.SetActive(true);
                break;
        }

        Time.timeScale = 0f; // Pause the game

        // Start the delay coroutine
        StartCoroutine(HandleGameOverAfterDelay(gameOverType, 5f));
    }

    // Coroutine to handle game-over actions after a delay
    private IEnumerator HandleGameOverAfterDelay(GameOverType gameOverType, float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Wait for the delay

        // Handle scene loading based on the game-over type
        if (gameOverType == GameOverType.WinGame)
        {
            LoadGameOverScene(); // Load the MainMenu scene for winning
        }
        else
        {
            ReloadScene(); // Reload the current scene for other game-over types
        }
    }

    // Reload the current scene
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Unpause the game
    }

    // Load the MainMenu scene
    public void LoadGameOverScene()
    {
        Cursor.lockState = CursorLockMode.None;
        BGMManager.instance.StopMusic();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f; // Unpause the game
    }
}