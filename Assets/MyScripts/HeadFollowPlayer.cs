using UnityEngine;

public class HeadLookController : MonoBehaviour
{
    public enum FollowMode { Nearby, Event }
    public enum RotationAxis { X, Y, Z, NegativeX, NegativeY, NegativeZ }

    [Header("References")]
    public Transform headBone;
    public Transform player;

    [Header("Settings")]
    public FollowMode followMode = FollowMode.Nearby;
    public float lookDistance = 10f;
    public float rotationSpeed = 5f;
    public float maxRotationAngle = 60f;

    [Header("Rotation Axis Setup")]
    [Tooltip("Axis for left/right rotation (usually X)")]
    public RotationAxis leftRightAxis = RotationAxis.X;
    [Tooltip("Axis for up/down rotation (usually Y)")]
    public RotationAxis upDownAxis = RotationAxis.Y;
    
    [Header("Direction Setup")]
    [Tooltip("Which local direction is the 'forward' of your head bone")]
    public Vector3 headForwardDirection = Vector3.down; // Negative Y is forward

    [Header("Debug")]
    public bool showDebugLogs = false;
    public bool showGizmos = false;

    private bool isFollowingByEvent = false;
    private Vector3 lastValidDirection;

    void Update()
    {
        if (headBone == null || player == null) return;
        
        bool shouldFollow = false;
        if (followMode == FollowMode.Nearby)
        {
            float distance = Vector3.Distance(player.position, headBone.position);
            shouldFollow = distance <= lookDistance;
        }
        else if (followMode == FollowMode.Event)
        {
            shouldFollow = isFollowingByEvent;
        }
        
        if (shouldFollow)
        {
            // Calculate direction to player
            Vector3 targetDir = (player.position - headBone.position).normalized;
            if (targetDir.sqrMagnitude < 0.001f) return;
            
            // Store last valid direction for smooth transitions
            lastValidDirection = targetDir;
            
            // Convert target direction to head bone local space
            Vector3 localDir = headBone.InverseTransformDirection(targetDir);
            
            // Calculate angles based on selected axes
            float leftRightAngle = CalculateAngleForAxis(localDir, leftRightAxis);
            float upDownAngle = CalculateAngleForAxis(localDir, upDownAxis);
            
            // Clamp the rotation angles
            leftRightAngle = Mathf.Clamp(leftRightAngle, -maxRotationAngle, maxRotationAngle);
            upDownAngle = Mathf.Clamp(upDownAngle, -maxRotationAngle, maxRotationAngle);
            
            // Create the desired local rotation
            Vector3 eulerAngles = Vector3.zero;
            eulerAngles = SetAngleForAxis(eulerAngles, leftRightAxis, leftRightAngle);
            eulerAngles = SetAngleForAxis(eulerAngles, upDownAxis, upDownAngle);
            
            // Apply the rotation
            Quaternion desiredLocalRotation = Quaternion.Euler(eulerAngles);
            
            // Smoothly rotate to the desired rotation
            headBone.localRotation = Quaternion.Slerp(
                headBone.localRotation,
                desiredLocalRotation,
                Time.deltaTime * rotationSpeed
            );
            
            if (showDebugLogs)
            {
                Debug.Log($"Local Direction: {localDir}");
                Debug.Log($"Left/Right Angle ({leftRightAxis}): {leftRightAngle}");
                Debug.Log($"Up/Down Angle ({upDownAxis}): {upDownAngle}");
            }
        }
    }

    private float CalculateAngleForAxis(Vector3 localDir, RotationAxis axis)
    {
        // For negative Y being forward, we need to adjust our angle calculations
        switch (axis)
        {
            case RotationAxis.X:
                return Mathf.Atan2(-localDir.z, -localDir.y) * Mathf.Rad2Deg;
            case RotationAxis.Y:
                return Mathf.Atan2(localDir.x, -localDir.y) * Mathf.Rad2Deg;
            case RotationAxis.Z:
                return Mathf.Atan2(localDir.x, localDir.z) * Mathf.Rad2Deg;
            case RotationAxis.NegativeX:
                return -Mathf.Atan2(-localDir.z, -localDir.y) * Mathf.Rad2Deg;
            case RotationAxis.NegativeY:
                return -Mathf.Atan2(localDir.x, -localDir.y) * Mathf.Rad2Deg;
            case RotationAxis.NegativeZ:
                return -Mathf.Atan2(localDir.x, localDir.z) * Mathf.Rad2Deg;
            default:
                return 0f;
        }
    }

    private Vector3 SetAngleForAxis(Vector3 eulerAngles, RotationAxis axis, float angle)
    {
        switch (axis)
        {
            case RotationAxis.X:
            case RotationAxis.NegativeX:
                eulerAngles.x = angle;
                break;
            case RotationAxis.Y:
            case RotationAxis.NegativeY:
                eulerAngles.y = angle;
                break;
            case RotationAxis.Z:
            case RotationAxis.NegativeZ:
                eulerAngles.z = angle;
                break;
        }
        return eulerAngles;
    }

    void OnDrawGizmos()
    {
        if (!showGizmos || headBone == null) return;

        // Draw head bone's local axes
        Gizmos.color = Color.red;
        Gizmos.DrawRay(headBone.position, headBone.right * 0.5f); // X axis
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(headBone.position, headBone.up * 0.5f); // Y axis
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(headBone.position, headBone.forward * 0.5f); // Z axis
        
        // Draw the head's "forward" direction (negative Y)
        Gizmos.color = Color.yellow;
        Vector3 headForward = headBone.TransformDirection(headForwardDirection);
        Gizmos.DrawRay(headBone.position, headForward * 0.7f);
        
        if (player != null)
        {
            // Draw line to player
            Gizmos.color = Color.white;
            Gizmos.DrawLine(headBone.position, player.position);
            
            // Draw target direction
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(headBone.position, lastValidDirection * 0.5f);
        }
    }

    public void ActivateFollow()
    {
        if (followMode == FollowMode.Event)
        {
            isFollowingByEvent = true;
            if (showDebugLogs) Debug.Log("Head follow activated by event.");
        }
    }

    public void DeactivateFollow()
    {
        if (followMode == FollowMode.Event)
        {
            isFollowingByEvent = false;
            if (showDebugLogs) Debug.Log("Head follow deactivated.");
        }
    }
}