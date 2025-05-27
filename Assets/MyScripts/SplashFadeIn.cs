using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashFadeIn : MonoBehaviour
{
    public CanvasGroup logoGroup;
    public float fadeDuration = 2f;
    public float holdTime = 1.5f; // time to hold the logo before fading out
    public string nextSceneName = "MainMenu"; // change to your scene's name

    private void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    private System.Collections.IEnumerator FadeRoutine()
    {
        // Fade in
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            logoGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        logoGroup.alpha = 1f;

        // Wait
        yield return new WaitForSeconds(holdTime);

        // Fade out
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            logoGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }

        logoGroup.alpha = 0f;

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
