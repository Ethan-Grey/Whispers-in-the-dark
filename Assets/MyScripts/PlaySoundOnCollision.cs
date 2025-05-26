// PlaySoundOnCollision.cs (SimpleImpactSound) – This script plays a sound (like a "bang" or "thud") when an object hits something. I put all the "play sound" logic in one place so I don't have to copy-paste everywhere – that's DRY!

using UnityEngine;

public class SimpleImpactSound : MonoBehaviour
{
    public AudioSource impactSound; // the sound (like a bang or thud) that plays when the object hits something

    // DRY: I put all the "play sound" logic in one place (here) so I don't have to copy-paste everywhere – it's like a shortcut!
    void OnCollisionEnter(Collision collision)
    {
        if (impactSound != null && !impactSound.isPlaying)
        {
            impactSound.Play(); // DRY: Sound playing logic is only here – so I don't have to repeat it everywhere!
        }
    }
}