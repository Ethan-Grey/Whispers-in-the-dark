using UnityEngine;
using UnityEngine.Events;

public class StatueController : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationIncrement = 45f; // Amount to rotate per interaction
    public float alignmentThreshold = 1f;
    public float targetRotationZ = 225.923f;

    [Header("Interaction")]
    [SerializeField] private bool canRotate = false; // Private by default, shown in Inspector
    public UnityEvent onStatueAligned;
    public Transform childSphere;

    private bool isAligned = false;

    /// <summary>
    /// Call this to allow the statue to rotate.
    /// </summary>
    public void EnableRotation()
    {
        canRotate = true;
        Debug.Log("🟢 Statue rotation ENABLED.");
    }

    /// <summary>
    /// Call this to prevent the statue from rotating.
    /// </summary>
    public void DisableRotation()
    {
        canRotate = false;
        Debug.Log("🔴 Statue rotation DISABLED.");
    }

    public void RotateStatue()
    {
        if (!canRotate || isAligned) return;

        Vector3 currentRotation = transform.eulerAngles;

        float newZ = (currentRotation.z + rotationIncrement) % 360f;

        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, newZ);

        CheckAlignment();
    }

    private void CheckAlignment()
    {
        float currentZ = transform.eulerAngles.z;
        float difference = Mathf.Abs(Mathf.DeltaAngle(currentZ, targetRotationZ));

        if (!isAligned && difference <= alignmentThreshold)
        {
            isAligned = true;
            onStatueAligned?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TargetSphere"))
        {
            Debug.Log("✅ Trigger event: Statue sphere collided with target sphere!");
            onStatueAligned?.Invoke();
        }
    }
}
