using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class HorrorLightFlicker : MonoBehaviour
{
    [Header("Light Settings")]
    [Tooltip("Single light - leave empty if using multiple lights")]
    public Light singleLight;
    [Tooltip("Multiple lights - use this for flickering several lights together")]
    public Light[] multipleLights;
    
    private List<float> originalIntensities = new List<float>();
    
    [Header("Flicker Settings")]
    public bool flickerOnStart = false;
    public float flickerDuration = 3f;
    public float minFlickerDelay = 0.05f;
    public float maxFlickerDelay = 0.3f;
    public float minIntensity = 0f;
    public float maxIntensity = 1.2f;
    
    [Header("Multiple Light Options")]
    [Tooltip("Should all lights flicker together or independently?")]
    public bool synchronizedFlicker = true;
    [Tooltip("Random delay between each light starting to flicker")]
    public float maxStartDelay = 0.5f;
    
    [Header("Audio (Optional)")]
    public AudioSource audioSource;
    public AudioClip[] flickerSounds;
    
    [Header("Unity Events")]
    public UnityEvent OnFlickerStart;
    public UnityEvent OnFlickerEnd;
    
    private bool isFlickering = false;
    private Coroutine flickerCoroutine;
    private List<Light> allLights = new List<Light>();
    
    void Start()
    {
        SetupLights();
        
        if (flickerOnStart)
            StartFlicker();
    }
    
    private void SetupLights()
    {
        allLights.Clear();
        originalIntensities.Clear();
        
        // Add single light if assigned
        if (singleLight != null)
        {
            allLights.Add(singleLight);
            originalIntensities.Add(singleLight.intensity);
        }
        
        // Add multiple lights if assigned
        if (multipleLights != null && multipleLights.Length > 0)
        {
            foreach (Light light in multipleLights)
            {
                if (light != null)
                {
                    allLights.Add(light);
                    originalIntensities.Add(light.intensity);
                }
            }
        }
        
        // If no lights assigned, try to get light from this GameObject
        if (allLights.Count == 0)
        {
            Light autoLight = GetComponent<Light>();
            if (autoLight != null)
            {
                allLights.Add(autoLight);
                originalIntensities.Add(autoLight.intensity);
            }
        }
    }
    
    [ContextMenu("Start Flicker")]
    public void StartFlicker()
    {
        if (!isFlickering && allLights.Count > 0)
        {
            flickerCoroutine = StartCoroutine(FlickerEffect());
        }
    }
    
    [ContextMenu("Stop Flicker")]
    public void StopFlicker()
    {
        if (isFlickering)
        {
            isFlickering = false;
            if (flickerCoroutine != null)
                StopCoroutine(flickerCoroutine);
            
            // Reset all lights to original state
            for (int i = 0; i < allLights.Count; i++)
            {
                if (allLights[i] != null && i < originalIntensities.Count)
                    allLights[i].intensity = originalIntensities[i];
            }
            
            OnFlickerEnd?.Invoke();
        }
    }
    
    [ContextMenu("Trigger Flicker")]
    public void TriggerFlicker()
    {
        if (!isFlickering)
            StartFlicker();
    }
    
    // Unity Event compatible methods
    public void StartFlickerEvent() => StartFlicker();
    public void StopFlickerEvent() => StopFlicker();
    public void TriggerFlickerEvent() => TriggerFlicker();
    
    private IEnumerator FlickerEffect()
    {
        isFlickering = true;
        OnFlickerStart?.Invoke();
        
        if (synchronizedFlicker)
        {
            yield return StartCoroutine(SynchronizedFlicker());
        }
        else
        {
            yield return StartCoroutine(IndependentFlicker());
        }
        
        // Reset all lights to original intensity
        for (int i = 0; i < allLights.Count; i++)
        {
            if (allLights[i] != null && i < originalIntensities.Count)
                allLights[i].intensity = originalIntensities[i];
        }
        
        isFlickering = false;
        OnFlickerEnd?.Invoke();
    }
    
    private IEnumerator SynchronizedFlicker()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < flickerDuration)
        {
            // All lights flicker with same intensity
            float flickerIntensity = Random.Range(minIntensity, maxIntensity);
            
            foreach (Light light in allLights)
            {
                if (light != null)
                    light.intensity = flickerIntensity;
            }
            
            PlayFlickerSound();
            
            float delay = Random.Range(minFlickerDelay, maxFlickerDelay);
            yield return new WaitForSeconds(delay);
            elapsedTime += delay;
        }
    }
    
    private IEnumerator IndependentFlicker()
    {
        // Start each light flickering with a random delay
        for (int i = 0; i < allLights.Count; i++)
        {
            if (allLights[i] != null)
            {
                float startDelay = Random.Range(0f, maxStartDelay);
                StartCoroutine(FlickerSingleLight(allLights[i], originalIntensities[i], startDelay));
            }
        }
        
        // Wait for the full duration
        yield return new WaitForSeconds(flickerDuration + maxStartDelay);
    }
    
    private IEnumerator FlickerSingleLight(Light light, float originalIntensity, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        
        float elapsedTime = 0f;
        while (elapsedTime < flickerDuration)
        {
            float flickerIntensity = Random.Range(minIntensity, maxIntensity);
            light.intensity = flickerIntensity;
            
            float delay = Random.Range(minFlickerDelay, maxFlickerDelay);
            yield return new WaitForSeconds(delay);
            elapsedTime += delay;
        }
    }
    
    private void PlayFlickerSound()
    {
        if (audioSource != null && flickerSounds.Length > 0)
        {
            if (Random.Range(0f, 1f) < 0.3f) // 30% chance to play sound
            {
                AudioClip randomSound = flickerSounds[Random.Range(0, flickerSounds.Length)];
                audioSource.PlayOneShot(randomSound);
            }
        }
    }
    
    // Trigger methods for collision detection
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerFlicker();
        }
    }
    
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isFlickering)
        {
            if (Random.Range(0f, 1f) < 0.01f) // 1% chance per frame
            {
                TriggerFlicker();
            }
        }
    }
}