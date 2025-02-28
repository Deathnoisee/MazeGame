using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.AI;
using System.Collections;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public TMP_Text text;
    public string interactionText;
    public UnityEvent onInteraction;
    private bool isInteracting = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void interact()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            HUDController.instance.DisableInteractionText();
            onInteraction.Invoke();
            StartCoroutine(ShowTextCoroutine());
        }
    }

    IEnumerator ShowTextCoroutine()
    {
        HUDController.instance.DisableInteractionText();
        if (text != null)
        {
            text.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(10);

        if (text != null) text.gameObject.SetActive(false);
        isInteracting = false;

        // Re-enable "Press E" text only if the outline is enabled and we're not interacting
        if (outline != null && outline.enabled && !isInteracting)
        {
            Debug.Log("Re-enabling 'Press E' text");
            HUDController.instance.EnableInteractionText();
        }
    }

    public void disableOutline()
    {
        outline.enabled = false;
    }

    public void enableOutline()
    {
        outline.enabled = true;
    }

    // Add this method to allow other scripts to check if the object is interacting
    public bool IsInteracting()
    {
        return isInteracting;
    }
}