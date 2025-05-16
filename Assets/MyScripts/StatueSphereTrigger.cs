using UnityEngine;
using UnityEngine.Events;

public class StatueSphereTrigger : MonoBehaviour
{
    [Header("Target Settings")]
    public string targetTag = "TargetSphere";

    [Header("Statue Settings")]
    public int statueIndex; // Unique index (e.g., 0, 1, 2)
    public StatueAlignmentManager manager; // Assign in Inspector!

    [Header("Optional Events")]
    public UnityEvent onSphereAligned;
    public UnityEvent onSphereExited;

    private bool isInside = false;

    private void OnTriggerStay(Collider other)
    {
        if (!isInside && other.CompareTag(targetTag))
        {
            isInside = true;
            Debug.Log($"✅ Statue {statueIndex} sphere aligned.");
            manager?.SetStatueAligned(statueIndex, true);
            onSphereAligned?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInside && other.CompareTag(targetTag))
        {
            isInside = false;
            Debug.Log($"❌ Statue {statueIndex} sphere exited.");
            manager?.SetStatueAligned(statueIndex, false);
            onSphereExited?.Invoke();
        }
    }
}
