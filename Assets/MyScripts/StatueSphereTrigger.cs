using UnityEngine;
using UnityEngine.Events;

public class StatueSphereTrigger : MonoBehaviour
{
    [Header("Target Settings")]
    public string targetTag = "TargetSphere"; // Make sure the target sphere is tagged with this

    [Header("Events")]
    public UnityEvent onSphereAligned;
    public UnityEvent onSphereExited;

    private bool isInside = false;

    private void OnTriggerStay(Collider other)
    {
        if (!isInside && other.CompareTag(targetTag))
        {
            isInside = true;
            Debug.Log("✅ Statue's child sphere is staying inside the target!");
            onSphereAligned?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInside && other.CompareTag(targetTag))
        {
            isInside = false;
            Debug.Log("❌ Statue's child sphere exited the target!");
            onSphereExited?.Invoke();
        }
    }
}
