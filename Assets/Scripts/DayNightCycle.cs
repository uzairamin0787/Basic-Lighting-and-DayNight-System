using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;

    public float rotationSpeed = 5f;

    [Header("Sun")]
    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.2f, 0.3f, 0.6f);

    public float dayIntensity = 1f;
    public float nightIntensity = 0.2f;

    [Header("Ambient Light")]
    public Color dayAmbient = new Color(0.7f, 0.7f, 0.7f);
    public Color nightAmbient = new Color(0.05f, 0.08f, 0.15f);

    void Update()
    {
        transform.Rotate(
            rotationSpeed * Time.deltaTime,
            0f,
            0f
        );

        float sunAmount =
            Vector3.Dot(transform.forward, Vector3.down);

        float value =
            Mathf.Clamp01((sunAmount + 1f) / 2f);

        // Sun color
        sun.color = Color.Lerp(
            nightColor,
            dayColor,
            value
        );

        // Sun brightness
        sun.intensity = Mathf.Lerp(
            nightIntensity,
            dayIntensity,
            value
        );

        // Environment ambient light
        RenderSettings.ambientLight = Color.Lerp(
            nightAmbient,
            dayAmbient,
            value
        );
    }
}