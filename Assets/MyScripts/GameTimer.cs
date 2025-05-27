using UnityEngine;
using TMPro;
using UHFPS.Runtime;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

public class GameTimer : MonoBehaviour, ISaveable // calls from i saveable to save the time
{
    [Header("UI References")]
    public TextMeshProUGUI timerText; // reference to the TMP text component in pause menu
    public string timeFormat = "Time: {0:D2}:{1:D2}:{2:D2}"; // format: HH:MM:SS

    private float totalSeconds = 0f;
    private bool isPaused = false;

    private void Start()
    {
        // Subscribe to pause events
        GameManager.SubscribePauseEvent(OnPauseStateChanged);
    }

    private void Update()
    {
        totalSeconds += Time.deltaTime; // updates the time 
        UpdateTimerDisplay(); // calls this method
    }

    // this feature was removed
    private void OnPauseStateChanged(bool paused) // when on paused state is on
    {
        isPaused = paused; // is paused
    }

    private void UpdateTimerDisplay() // updates the text mesh in game so the time is displayed
    {
        if (timerText != null)
        {
            int hours = Mathf.FloorToInt(totalSeconds / 3600); // gets the hour
            int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60); // gets the minutes
            int seconds = Mathf.FloorToInt(totalSeconds % 60); // gets the seconds
            
            timerText.text = string.Format(timeFormat, hours, minutes, seconds); // sets the format to the previosuly retrieved data then sets thaat to the text mesh in game
        }
    }

    // ISaveable implementation makes sure the time is saved and loaded when needed
    public StorableCollection OnSave() // grabs the on save colection
    {
        return new StorableCollection()
        {
            { "totalSeconds", totalSeconds } // returns the storable
        };
    }

    public void OnLoad(JToken data) // when saved game is loaded
    {
        totalSeconds = (float)data["totalSeconds"];
        UpdateTimerDisplay();
    }
} 