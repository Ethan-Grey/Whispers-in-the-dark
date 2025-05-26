using UnityEngine;
using TMPro;
using UHFPS.Runtime;
using Newtonsoft.Json.Linq;

public class GameTimer : MonoBehaviour, ISaveable
{
    [Header("UI References")]
    public TextMeshProUGUI timerText; // Reference to the TMP text component in pause menu
    public string timeFormat = "Time: {0:D2}:{1:D2}:{2:D2}"; // Format: HH:MM:SS

    private float totalSeconds = 0f;
    private bool isPaused = false;

    private void Start()
    {
        // Subscribe to pause events
        GameManager.SubscribePauseEvent(OnPauseStateChanged);
    }

    private void Update()
    {
        if (!isPaused)
        {
            totalSeconds += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void OnPauseStateChanged(bool paused)
    {
        isPaused = paused;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int hours = Mathf.FloorToInt(totalSeconds / 3600);
            int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
            int seconds = Mathf.FloorToInt(totalSeconds % 60);
            
            timerText.text = string.Format(timeFormat, hours, minutes, seconds);
        }
    }

    // ISaveable implementation
    public StorableCollection OnSave()
    {
        return new StorableCollection()
        {
            { "totalSeconds", totalSeconds }
        };
    }

    public void OnLoad(JToken data)
    {
        totalSeconds = (float)data["totalSeconds"];
        UpdateTimerDisplay();
    }
} 