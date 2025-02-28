using UnityEngine;

    interface IInteractable
    {
        void Interact();
    }
public class Interactor : MonoBehaviour
{
    public Transform Interactorsource;
    public float Interactrange;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(Interactorsource.position, Interactorsource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, Interactrange))
        {
          if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
          {
            interactObj.Interact();
          }
        }
      
    }
}
