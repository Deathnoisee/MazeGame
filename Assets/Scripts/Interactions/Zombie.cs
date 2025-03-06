using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
    public gameOver gameOverHandler;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            gameOverHandler.gg(gameOver.GameOverType.ZombieTouch);
        }
    }
}