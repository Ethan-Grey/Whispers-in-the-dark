// SceneLoader.cs – This script lets you load a new scene (like a new level) after a delay. I put all the “wait and then load” stuff in one coroutine so I don’t have to copy-paste everywhere – that’s DRY!

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public float delayBeforeSceneLoad = 2f; // how long to wait before loading the new scene
    public string sceneName = ""; // the name of the scene you want to load

    // DRY: I put all the "wait and then load" logic in one coroutine (LoadEndingScene) so I don't have to copy-paste everywhere – it's like a shortcut!
    public void StartSceneLoad()
    {
        StartCoroutine(LoadEndingScene()); // DRY: Always uses the same coroutine for delay and loading
    }

    // DRY: I put all the "wait and then load" logic in one coroutine so I don't have to copy-paste everywhere – it's like a shortcut!
    IEnumerator LoadEndingScene()
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad); // wait for the delay
        SceneManager.LoadScene(sceneName); // DRY: Scene loading logic is centralized here
    }
}
