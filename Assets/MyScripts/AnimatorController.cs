using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    // This can be called from Unity Events (e.g., buttons, triggers)
    public void StopAnimation()
    {
        if (animator != null)
        {
            animator.enabled = false; // Disables the Animator
            // OR, if you want to just stop playback:
            // animator.StopPlayback();
        }
    }
}
