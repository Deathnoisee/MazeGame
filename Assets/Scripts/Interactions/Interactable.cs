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

    public void Interact()
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

        if (outline != null && outline.enabled && !isInteracting)
        {
            Debug.Log("Re-enabling 'Press E' text");
            HUDController.instance.EnableInteractionText();
        }
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }
}