using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public float delayBeforeSceneLoad = 2f;
    public string sceneName = "";

    // This method will be called from the Signal Emitter
    public void StartSceneLoad()
    {
        StartCoroutine(LoadEndingScene());
    }

    IEnumerator LoadEndingScene()
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneManager.LoadScene(sceneName);
    }
}
