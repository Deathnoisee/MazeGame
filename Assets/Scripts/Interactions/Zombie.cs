using UnityEngine;

public class ZombieCollision : MonoBehaviour
{
    public gameOver gameOverHandler;
    public Animator test;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            gameOverHandler.gg(gameOver.GameOverType.ZombieTouch);
        }
        if (other.CompareTag("test")) 
        {
            test.SetBool("jump", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ( other.CompareTag("test")) 
        {
            test.SetBool("jump", false);
        }
    }
}