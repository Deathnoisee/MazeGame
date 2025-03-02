using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene or loading a new one

public class gameOver : MonoBehaviour
{
    public enum GameOverType
    {
        ZombieTouch, // Player touched by a zombie
        TrapFall,    // Player fell into a trap
        WinGame      // Player opened the ending gate and won
    }

    // Method to handle game over
    public void gg(GameOverType gameOverType)
    {
        switch (gameOverType)
        {
            case GameOverType.ZombieTouch:
                Debug.Log("Game Over: You were killed by a zombie!");
                // Trigger zombie death animation, sound, or UI
                break;

            case GameOverType.TrapFall:
                Debug.Log("Game Over: You fell into a trap!");
                // Trigger trap death animation, sound, or UI
                break;

            case GameOverType.WinGame:
                Debug.Log("You Win: You escaped!");
                // Trigger win animation, sound, or UI
                break;
        }

        // Reload the scene or load a game-over scene
        ReloadScene();
    }

    // Reload the current scene
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: Load a specific game-over scene
    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene"); // Replace with your game-over scene name
    }
}