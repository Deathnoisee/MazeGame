using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public static LeverManager instance;

    private int leversActivated = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LeverActivated()
    {
        leversActivated++;
    }

    public bool AreAllLeversActivated()
    {
        return leversActivated >= 3;
    }
}