using System.Collections;
using UnityEngine;

/// <summary>
/// Makes a light component randomly flicker on and off regardless of the object's active state.
/// </summary>
public class AutoRandomLightFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    [Tooltip("Minimum time the light stays on")]
    [SerializeField] private float minOnTime = 0.1f;
    
    [Tooltip("Maximum time the light stays on")]
    [SerializeField] private float maxOnTime = 0.5f;
    
    [Tooltip("Minimum time the light stays off")]
    [SerializeField] private float minOffTime = 0.05f;
    
    [Tooltip("Maximum time the light stays off")]
    [SerializeField] private float maxOffTime = 0.2f;
    
    [Tooltip("Chance of flickering (0-1). Higher values make flickering more frequent")]
    [Range(0f, 1f)]
    [SerializeField] private float flickerChance = 0.7f;
    
    [Tooltip("Light intensity variation")]
    [Range(0f, 1f)]
    [SerializeField] private float intensityVariation = 0.2f;

    // Reference to the light component
    private Light lightComponent;
    
    // Original light intensity
    private float baseIntensity;
    
    // Track if we're currently flickering
    private bool isFlickering = false;

    private void Awake()
    {
        // Get reference to the light component
        lightComponent = GetComponent<Light>();
        
        // If no light component was found, try to find one in children
        if (lightComponent == null)
        {
            lightComponent = GetComponentInChildren<Light>();
            
            // Log a warning if still no light component was found
            if (lightComponent == null)
            {
                Debug.LogWarning("RandomLightFlicker script attached to " + gameObject.name + " but no Light component found!");
                return;
            }
        }
        
        // Store base intensity
        baseIntensity = lightComponent.intensity;
    }

    private void OnEnable()
    {
        // Start the flickering coroutine
        StartCoroutine(FlickerLight());
    }

    private void Start()
    {
        // If the script wasn't enabled when awake, make sure we start flickering now
        if (!isFlickering && gameObject.activeInHierarchy)
        {
            StartCoroutine(FlickerLight());
        }
    }

    private IEnumerator FlickerLight()
    {
        isFlickering = true;
        
        while (true)
        {
            // Random decision to flicker based on chance
            if (Random.value <= flickerChance)
            {
                // Turn light off or significantly dim it
                float dimIntensity = baseIntensity * Random.Range(0f, 0.2f);
                lightComponent.intensity = dimIntensity;
                
                // Wait for random off time
                yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));
                
                // Turn light back on with slight random intensity variation
                float onIntensity = baseIntensity * Random.Range(1f - intensityVariation, 1f + intensityVariation);
                lightComponent.intensity = onIntensity;
            }
            else
            {
                // Small intensity variation
                lightComponent.intensity = baseIntensity * Random.Range(0.9f, 1.1f);
            }
            
            // Wait for random on time
            yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));
        }
    }

    private void OnDisable()
    {
        // Stop flickering when disabled
        StopAllCoroutines();
        isFlickering = false;
        
        // Reset light intensity to base value
        if (lightComponent != null)
        {
            lightComponent.intensity = baseIntensity;
        }
    }
    
    // This can be called externally to temporarily stop flickering
    public void StopFlicker()
    {
        StopAllCoroutines();
        isFlickering = false;
        if (lightComponent != null)
        {
            lightComponent.intensity = baseIntensity;
        }
    }
    
    // This can be called externally to restart flickering
    public void StartFlicker()
    {
        if (!isFlickering)
        {
            StartCoroutine(FlickerLight());
        }
    }
}