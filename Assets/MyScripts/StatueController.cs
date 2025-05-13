using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatuesController : MonoBehaviour
{
    [System.Serializable]
    public class StatueData
    {
        public string conditionName;               // The condition name to unlock rotation
        public GameObject statue;                  // Reference to statue GameObject
        public float rotationAmount = 90f;         // How much it rotates when triggered
        public float targetZRotation;              // The target Z rotation (this is the specific angle)
        [HideInInspector] public bool canRotate = false;
        [HideInInspector] public bool hasReachedTarget = false;
    }

    public List<StatueData> statues = new List<StatueData>();
    public UnityEvent onAllStatuesCorrectlyRotated; // The UnityEvent to call when all statues are rotated

    // Call this to unlock rotation for a specific statue
    public void EnableRotationByCondition(string conditionName)
    {
        foreach (var statue in statues)
        {
            if (statue.conditionName.Equals(conditionName, System.StringComparison.OrdinalIgnoreCase))
            {
                statue.canRotate = true;
                Debug.Log($"Rotation enabled for: {statue.conditionName}");
                return;
            }
        }

        Debug.LogWarning($"No statue found with condition name: {conditionName}");
    }

    // Call this from UHFPS or a key press to rotate a statue
    public void RotateStatue(int index)
    {
        if (index < 0 || index >= statues.Count) return;

        StatueData statue = statues[index];

        if (!statue.canRotate || statue.hasReachedTarget) return;

        statue.statue.transform.Rotate(0f, 0f, statue.rotationAmount);

        float currentZ = NormalizeAngle(statue.statue.transform.eulerAngles.z);
        float targetZ = NormalizeAngle(statue.targetZRotation);

        if (Mathf.Abs(Mathf.DeltaAngle(currentZ, targetZ)) < 1f)
        {
            statue.hasReachedTarget = true;
            statue.canRotate = false;
            Debug.Log($"{statue.conditionName} reached target rotation!");
        }

        CheckAllStatues();
    }

    void CheckAllStatues()
    {
        foreach (var statue in statues)
        {
            if (!statue.hasReachedTarget)
                return;
        }

        Debug.Log("All statues are correctly rotated!");
        onAllStatuesCorrectlyRotated.Invoke(); // This will invoke the UnityEvent when all statues are rotated correctly
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }
}
