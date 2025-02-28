using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] TMP_Text interactionText;
    public void EnableInteractionText ()
    {
        interactionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText ()
    {
        interactionText.gameObject.SetActive(false);
    }

}
