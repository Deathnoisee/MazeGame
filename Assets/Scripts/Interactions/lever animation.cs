using UnityEngine;

public class leveranimation : MonoBehaviour
{
    public Animator animator;
    private bool isActivated = false;

    public void Interact()
    {
        if (!isActivated)
        {
            animator.SetBool("clicked", true);
            isActivated = true;
            LeverManager.instance.LeverActivated();
        }
    }
}