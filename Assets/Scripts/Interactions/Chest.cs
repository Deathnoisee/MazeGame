using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Animator animator;

    private bool isOpen = false;
    public void Interact()
    {
        isOpen = !isOpen;
        animator.SetBool("Open",isOpen); 
        
    }
}
