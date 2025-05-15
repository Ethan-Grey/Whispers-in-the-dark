using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Jumpscare : MonoBehaviour
{
    public GameObject watcher;
    public GameObject lightsource;
    public AudioSource jumpscareSound;
    public float scareDuration = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerJumpscare());
        }
    }

    IEnumerator TriggerJumpscare()
    {
        jumpscareSound.Play();
        watcher.SetActive(true);
        lightsource.SetActive(true);
        yield return new WaitForSeconds(scareDuration);
        watcher.SetActive(false);
        lightsource.SetActive(false);
        Destroy(gameObject); // Remove trigger
    }
}
