// TriggerFallingObject.cs – This script lets you "drop" an object (like a statue or a rock) when a player (or any object with the "Player" tag) enters a trigger. I put all the "drop" logic in one place so I don't have to copy-paste everywhere – that's DRY!

using UnityEngine;

public class TriggerFallingObject : MonoBehaviour
{
    public Rigidbody fallingObject; // the object (like a statue) that you want to "drop" (by turning off kinematic)

    // DRY: I put all the "drop" logic in one place (here) so I don' have to copy-paste everywhere – it's like a shortcut!
    private void OnTriggerEnter(Collider other) // detects when trigger has been entered
    {
        if (other.CompareTag("Player")) // ensure trigger was entered by a player
        {
            fallingObject.isKinematic = false; // DRY: Trigger (drop) logic is centralized here – so I don't have to repeat it everywhere!
            Destroy(gameObject); // Optional: remove trigger after (so it doesn't trigger again)
        }
    }
}