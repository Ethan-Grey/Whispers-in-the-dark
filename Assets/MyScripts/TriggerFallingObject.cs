using UnityEngine;

public class TriggerFallingObject : MonoBehaviour
{
    public Rigidbody fallingObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fallingObject.isKinematic = false;
            Destroy(gameObject); // Optional: remove trigger after
        }
    }
}