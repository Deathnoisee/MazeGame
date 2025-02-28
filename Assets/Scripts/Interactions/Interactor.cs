using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private Transform InteractroSource;
    [SerializeField]
    private float interactrorRange = 4f;
    [SerializeField]
    private GameObject interactionText;
    private Ray r;

    void Update()
    {

        r = new Ray(InteractroSource.position, InteractroSource.forward);
        Debug.DrawRay(InteractroSource.position, InteractroSource.forward * interactrorRange, Color.red, 2f);

        if (Physics.Raycast(r, out RaycastHit hitInfo, interactrorRange))
        {
            

            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                Debug.Log("in range");
                interactionText.SetActive(true);    
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                interactionText.SetActive(false);
                Debug.Log("No interactable obj");
            }

        }
        else
        {
            interactionText.SetActive(false);
        }
     
    }
}
