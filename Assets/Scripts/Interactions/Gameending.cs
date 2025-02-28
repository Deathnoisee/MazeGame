using UnityEngine;

public class Gameending : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private bool isOpen = false;
    public void interact ()
    {
        isOpen = !isOpen;
        animator.SetBool("WIN", true);
    }
}
