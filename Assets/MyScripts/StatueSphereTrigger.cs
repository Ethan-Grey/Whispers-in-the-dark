using System.CodeDom;
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

    private void OnTriggerStay(Collider other) // when trigger stay inside the collision area
    {
        if (!isInside && other.CompareTag(targetTag)) // checks for conditions before continuing
        {
            isInside = true; // when conditions are met sets isinside to true
            Debug.Log($"✅ Statue {statueIndex} sphere aligned."); // logs 
            manager?.SetStatueAligned(statueIndex, true); // references the statue manager to say that this statue is aligned
            onSphereAligned?.Invoke(); // invokes the on sphere align event called in the inspector
        }
    }

    private void OnTriggerExit(Collider other) // when trigger exits collision zone
    {
        if (isInside && other.CompareTag(targetTag)) // conditions
        {
            // does the aposite of all the things above
            isInside = false;
            Debug.Log($"❌ Statue {statueIndex} sphere exited.");
            manager?.SetStatueAligned(statueIndex, false);
            onSphereExited?.Invoke(); // calls the exit sphere event in inspector
        }
    }
}
