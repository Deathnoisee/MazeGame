using UnityEngine;

public class SmoothFlickeringLight : MonoBehaviour
{
    private Light flickerLight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 2.0f;
    public float flickerSpeed = 2.0f; // Smooth transition speed

    void Start()
    {
        flickerLight = GetComponent<Light>();
    }

    void Update()
    {
        if (flickerLight != null)
        {
            float targetIntensity = Random.Range(minIntensity, maxIntensity);
            flickerLight.intensity = Mathf.Lerp(flickerLight.intensity, targetIntensity, flickerSpeed * Time.deltaTime);
        }
    }
}
