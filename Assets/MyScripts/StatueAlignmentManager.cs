using UnityEngine;
using UnityEngine.Events;

public class StatueAlignmentManager : MonoBehaviour
{
    [Header("Statue Alignment Settings")]
    public int totalStatues = 3;

    [Tooltip("This UnityEvent will trigger once all statues are aligned.")]
    public UnityEvent onAllStatuesAligned;

    private bool[] alignedStatues;
    private bool hasTriggered = false;

    private void Awake()
    {
        alignedStatues = new bool[totalStatues];
    }

    public void SetStatueAligned(int index, bool isAligned)
    {
        alignedStatues[index] = isAligned;

        if (!hasTriggered && AllStatuesAligned())
        {
            hasTriggered = true;
            Debug.Log("✅ All statues are aligned!");
            onAllStatuesAligned?.Invoke(); // 👈 This shows up in the Inspector!
        }
    }

    private bool AllStatuesAligned()
    {
        foreach (bool aligned in alignedStatues)
        {
            if (!aligned) return false;
        }
        return true;
    }
}
