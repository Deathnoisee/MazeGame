using UnityEngine;
using TMPro;
using System.Collections;

public class Gameending : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator gate;
    [SerializeField] private TMP_Text messageText;
    private bool isOpen = false;

    public void Interact()
    {
        if (LeverManager.instance.AreAllLeversActivated())
        {
            Debug.Log("All levers activated. Ending lever is functional.");
            isOpen = !isOpen;
            gate.SetBool("WIN", true);
            animator.SetBool("WIN", true);

            
        }
        else
        {
            Debug.Log("Not all levers are activated. Showing message.");
            if (messageText != null)
            {
                Debug.Log("Message text is assigned. Displaying message.");
                messageText.text = "You need to open all 3 levers first!";
                messageText.gameObject.SetActive(true);
                StartCoroutine(HideMessageAfterDelay(3f)); // Hide the message after 3 seconds
            }
            else
            {
                Debug.LogError("Message text is not assigned in the Inspector.");
            }
        }
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageText != null)
        {
            Debug.Log("Hiding message text.");
            messageText.gameObject.SetActive(false);
        }
    }
}