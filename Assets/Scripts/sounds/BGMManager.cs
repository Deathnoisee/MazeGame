using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance; // Singleton instance

    public AudioSource bgmAudioSource; // Reference to the AudioSource component

    private void Awake()
    {
        // Ensure only one instance of BGMManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this GameObject persistent
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }

        // Get the AudioSource component
        bgmAudioSource = GetComponent<AudioSource>();
    }

    // Smoothly adjust the pitch of the BGM
    public IEnumerator AdjustPitchSmoothly(float targetPitch, float duration)
    {
        float startPitch = bgmAudioSource.pitch; // Current pitch
        float elapsed = 0f; // Time elapsed

        while (elapsed < duration)
        {
            // Interpolate the pitch over time
            bgmAudioSource.pitch = Mathf.Lerp(startPitch, targetPitch, elapsed / duration);
            elapsed += Time.deltaTime; // Increment time
            yield return null; // Wait for the next frame
        }

        // Ensure the final pitch is set
        bgmAudioSource.pitch = targetPitch;
    }

    // Reset the pitch to normal (1.0f)
    public void ResetBGMPitch()
    {
        bgmAudioSource.pitch = 1.0f; // Reset to normal speed
    }
}