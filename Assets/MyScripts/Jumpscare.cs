using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jumpscare : MonoBehaviour
{
    public GameObject watcher; // sets game onject as the monster
    public GameObject lightsource; // sets gameobject as the light source
    public AudioSource jumpscareSound; // references the audio source with the jumpscare sound
    public float scareDuration = 0.5f; // sets a public float for how long the scare lasts

    private void OnTriggerEnter(Collider other) // calls this method when the object trigger has been hit
    {
        if (other.CompareTag("Player")) // ensures the proceding code only runs if the collision that entered the trigger was a player object
        {
            StartCoroutine(TriggerJumpscare()); // start the Ienumerator triggerjumpscare below
        }
    }

    IEnumerator TriggerJumpscare() 
    {
        jumpscareSound.Play(); // plays jumpscare sound
        watcher.SetActive(true); // sets monster object as active
        lightsource.SetActive(true); // sets light source active
        yield return new WaitForSeconds(scareDuration); // waits for the time indecated by the inspector
        // undoes all of the above
        watcher.SetActive(false); 
        lightsource.SetActive(false);
        Destroy(gameObject); // Remove trigger
    }
}
