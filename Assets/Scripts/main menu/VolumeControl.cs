using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the slider UI component

    void Start()
    {
        
        volumeSlider.value = AudioListener.volume;
        
        
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    
    void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
