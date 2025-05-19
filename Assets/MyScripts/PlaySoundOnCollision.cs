using UnityEngine;

public class SimpleImpactSound : MonoBehaviour
{
    public AudioSource impactSound;

    void OnCollisionEnter(Collision collision)
    {
        if (impactSound != null && !impactSound.isPlaying)
        {
            impactSound.Play();
        }
    }
}