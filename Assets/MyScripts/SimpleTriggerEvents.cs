// SimpleTriggerEvents.cs – This script lets you trigger events (like playing a sound or activating something) when a player (or any object) enters, exits, or stays in a trigger. I put all the "should I trigger?" logic in one place so I don't have to copy-paste everywhere – that's DRY!

using UnityEngine;
using UnityEngine.Events;

public class SimpleTriggerEvents : MonoBehaviour
{
    [Header("Trigger Settings")]
    public string targetTag = "Player"; // the tag (like "Player") that triggers the event
    public bool useAnyObject = false; // if true, any object (even without a tag) triggers the event

    [Header("Events")]
    public UnityEvent OnTriggerEnterEvent; // this event runs when something enters the trigger
    public UnityEvent OnTriggerExitEvent;  // this event runs when something exits the trigger
    public UnityEvent OnTriggerStayEvent;  // this event runs every frame while something is in the trigger

    [Header("Debug")]
    public bool showDebugMessages = false; // if true, it prints messages in the console

    // DRY: I put all the "should I trigger?" logic in one method so I don't have to copy-paste it everywhere – it's like a shortcut!
    private bool ShouldTrigger(Collider other)
    {
        if (useAnyObject)
            return true; // if useAnyObject is on, trigger for any object
        return other.CompareTag(targetTag); // otherwise, only trigger if the object has the right tag (like "Player")
    }

    // DRY: I use ShouldTrigger() here so I don't have to repeat the tag check everywhere – it's all in one place!
    private void OnTriggerEnter(Collider other)
    {
        if (ShouldTrigger(other))
        {
            if (showDebugMessages)
                Debug.Log($"{other.name} entered trigger"); // print a message if debug is on
            OnTriggerEnterEvent?.Invoke(); // run the "enter" event
        }
    }

    // DRY: I use ShouldTrigger() here so I don't have to repeat the tag check everywhere – it's all in one place!
    private void OnTriggerExit(Collider other)
    {
        if (ShouldTrigger(other))
        {
            if (showDebugMessages)
                Debug.Log($"{other.name} exited trigger"); // print a message if debug is on
            OnTriggerExitEvent?.Invoke(); // run the "exit" event
        }
    }

    // DRY: I use ShouldTrigger() here so I don't have to repeat the tag check everywhere – it's all in one place!
    private void OnTriggerStay(Collider other)
    {
        if (ShouldTrigger(other))
        {
            OnTriggerStayEvent?.Invoke(); // run the "stay" event every frame
        }
    }

    // DRY: I put these public methods here so I can call the same events from other scripts or the inspector – it's like a shortcut!
    public void TriggerEnterEvent()
    {
        OnTriggerEnterEvent?.Invoke(); // run the "enter" event (centralized event invocation)
    }

    public void TriggerExitEvent()
    {
        OnTriggerExitEvent?.Invoke(); // run the "exit" event (centralized event invocation)
    }

    public void TriggerStayEvent()
    {
        OnTriggerStayEvent?.Invoke(); // run the "stay" event (centralized event invocation)
    }
}