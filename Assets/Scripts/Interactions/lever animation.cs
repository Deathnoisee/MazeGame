using UnityEngine;

public class leveranimation : MonoBehaviour
{
    public Animator animator;

    public void interact ()
    {
        animator.SetBool("clicked", true);
    }
}
