using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UHFPS.Runtime;

public class PuzzleTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 60f;
    public bool startOnAwake = false;
    public bool showDebugTimer = true;
    
    [Header("Warning Settings")]
    public bool enableWarning = true;
    public float warningTime = 10f; // Warning when 10 seconds left
    
    [Header("Events")]
    public UnityEvent OnTimerStart;
    public UnityEvent OnTimerWarning;
    public UnityEvent OnTimerEnd;
    public UnityEvent<float> OnTimerUpdate; // Sends remaining time
    
    private float currentTime;
    private bool isRunning = false;
    private bool warningTriggered = false;
    private PuzzleBase connectedPuzzle;
    
    void Start()
    {
        // Try to find puzzle on same GameObject
        connectedPuzzle = GetComponent<PuzzleBase>();
        
        if (startOnAwake)
        {
            StartTimer();
        }
    }
    
    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            
            // Send timer update
            OnTimerUpdate?.Invoke(currentTime);
            
            // Check for warning
            if (enableWarning && !warningTriggered && currentTime <= warningTime)
            {
                warningTriggered = true;
                OnTimerWarning?.Invoke();
                
                if (showDebugTimer)
                    Debug.Log($"Puzzle Timer Warning: {warningTime} seconds remaining!");
            }
            
            // Check if time is up
            if (currentTime <= 0f)
            {
                TimeUp();
            }
            
            // Debug display
            if (showDebugTimer)
            {
                Debug.Log($"Puzzle Time Remaining: {currentTime:F1}s");
            }
        }
    }
    
    public void StartTimer()
    {
        currentTime = timeLimit;
        isRunning = true;
        warningTriggered = false;
        
        OnTimerStart?.Invoke();
        
        if (showDebugTimer)
            Debug.Log($"Puzzle Timer Started: {timeLimit} seconds");
    }
    
    public void StopTimer()
    {
        isRunning = false;
        
        if (showDebugTimer)
            Debug.Log("Puzzle Timer Stopped");
    }
    
    public void ResetTimer()
    {
        currentTime = timeLimit;
        warningTriggered = false;
        
        if (showDebugTimer)
            Debug.Log("Puzzle Timer Reset");
    }
    
    public void AddTime(float extraTime)
    {
        currentTime += extraTime;
        
        if (showDebugTimer)
            Debug.Log($"Added {extraTime} seconds to timer");
    }
    
    private void TimeUp()
    {
        isRunning = false;
        OnTimerEnd?.Invoke();
        
        // Automatically kick player out if puzzle is connected
        if (connectedPuzzle != null)
        {
            KickPlayerOut();
        }
        
        if (showDebugTimer)
            Debug.Log("Puzzle Timer: Time's Up!");
    }
    
    private void KickPlayerOut()
    {
        // Use reflection to call SwitchBack method since it's protected
        var switchBackMethod = connectedPuzzle.GetType().GetMethod("SwitchBack", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
        if (switchBackMethod != null)
        {
            switchBackMethod.Invoke(connectedPuzzle, null);
            Debug.Log("Player kicked out due to time limit!");
        }
    }
    
    // Public properties for other scripts
    public float RemainingTime => currentTime;
    public float RemainingTimePercent => currentTime / timeLimit;
    public bool IsRunning => isRunning;
    public bool IsWarningActive => warningTriggered;
}