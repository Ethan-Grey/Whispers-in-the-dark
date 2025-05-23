using UnityEngine;
using UnityEngine.Events;

public class SimpleTriggerEvents : MonoBehaviour
{
    [Header("Trigger Settings")]
    public string targetTag = "Player";
    public bool useAnyObject = false;

    [Header("Events")]
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerExitEvent;
    public UnityEvent OnTriggerStayEvent;

    [Header("Debug")]
    public bool showDebugMessages = false;

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldTrigger(other))
        {
            if (showDebugMessages)
                Debug.Log($"{other.name} entered trigger");
                
            OnTriggerEnterEvent?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ShouldTrigger(other))
        {
            if (showDebugMessages)
                Debug.Log($"{other.name} exited trigger");
                
            OnTriggerExitEvent?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (ShouldTrigger(other))
        {
            OnTriggerStayEvent?.Invoke();
        }
    }

    private bool ShouldTrigger(Collider other)
    {
        if (useAnyObject)
            return true;
            
        return other.CompareTag(targetTag);
    }

    // Public methods you can call from other scripts
    public void TriggerEnterEvent()
    {
        OnTriggerEnterEvent?.Invoke();
    }

    public void TriggerExitEvent()
    {
        OnTriggerExitEvent?.Invoke();
    }

    public void TriggerStayEvent()
    {
        OnTriggerStayEvent?.Invoke();
    }
}