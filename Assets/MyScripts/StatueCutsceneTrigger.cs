using System.Collections;
using UnityEngine;

public class StatueRevealManager : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;               // Your scene's main camera
    public Transform[] statueCamPoints;     // One empty GameObject per statue, placed where you want the camera
    public AudioSource revealAudio;         // The sound to play when revealing

    [Header("Timing")]
    public float moveDuration = 1f;         // How long to interpolate TO the statue
    public float revealHoldTime = 3f;       // How long to stay on the statue

    private Vector3 originalPos;
    private Quaternion originalRot;
    private bool busy = false;

    void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    /// <summary>
    /// Call this when a puzzle is solved, passing the index of the statue (0,1,2).
    /// </summary>
    public void RevealStatue(int statueIndex)
    {
        if (busy || statueIndex < 0 || statueIndex >= statueCamPoints.Length) return;
        StartCoroutine(RevealRoutine(statueIndex));
    }

    IEnumerator RevealRoutine(int idx)
    {
        busy = true;

        // 1) Save original transform
        originalPos = mainCamera.transform.position;
        originalRot = mainCamera.transform.rotation;

        // 2) Optionally play sound at the start
        if (revealAudio != null) revealAudio.Play();

        // 3) Move camera TO statue
        Transform target = statueCamPoints[idx];
        yield return StartCoroutine(MoveCam(mainCamera.transform, target.position, target.rotation, moveDuration));

        // 4) Hold for revealHoldTime
        yield return new WaitForSeconds(revealHoldTime);

        // 5) Move back to original
        yield return StartCoroutine(MoveCam(mainCamera.transform, originalPos, originalRot, moveDuration));

        busy = false;
    }

    IEnumerator MoveCam(Transform cam, Vector3 destPos, Quaternion destRot, float duration)
    {
        Vector3 startPos = cam.position;
        Quaternion startRot = cam.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.SmoothStep(0f,1f, elapsed / duration);
            cam.position = Vector3.Lerp(startPos, destPos, t);
            cam.rotation = Quaternion.Slerp(startRot, destRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap into place
        cam.position = destPos;
        cam.rotation = destRot;
    }
}
