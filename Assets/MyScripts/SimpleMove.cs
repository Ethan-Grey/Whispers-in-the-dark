// SimpleMove.cs – This script makes an object move forward (like a character) and also plays an animation. I put all the "start" and "stop" stuff in one place so I don't have to copy-paste everywhere – that's DRY!

using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float moveSpeed = 3f; // how fast the object moves
    public float moveDistance = 9f; // how far it goes
    public bool autoStart = false; // if true, it starts moving as soon as the game starts

    private Animator animator; // this is for playing animations
    private float distanceMoved; // keeps track of how far we've moved so far
    private bool isMoving = false; // tells us if the object is moving or not
    private Vector3 startPosition; // where the object started from

    void Start()
    {
        animator = GetComponent<Animator>(); // grab the animator so we can play animations
        startPosition = transform.position; // remember where we started
        
        if (autoStart)
        {
            StartMoving(); // if autoStart is on, start moving right away
        }
    }

    void Update()
    {
        // DRY: I put the movement logic in Update so I don't have to repeat it everywhere – it's all in one place!
        if (isMoving)
        {
            if (distanceMoved < moveDistance)
            {
                float moveAmount = moveSpeed * Time.deltaTime; // figure out how far to move this frame
                transform.position += transform.forward * moveAmount; // move the object forward
                distanceMoved += moveAmount; // update how far we've gone
            }
            else
            {
                StopMoving(); // if we've gone far enough, stop moving
            }
        }
    }

    // DRY: I put all the "start moving" stuff in one method so I don't have to copy-paste it everywhere – it's like a shortcut!
    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true; // tell the script we're moving now
            distanceMoved = 0f; // reset how far we've gone
            startPosition = transform.position; // update our start position (just in case)
            
            if (animator != null)
                animator.SetBool("isRunning", true); // play the "running" animation if we have an animator
        }
    }

    // DRY: I put all the "stop moving" stuff in one method so I don't have to copy-paste it everywhere – it's like a shortcut!
    public void StopMoving()
    {
        isMoving = false; // tell the script we're not moving anymore
        
        if (animator != null)
            animator.SetBool("isRunning", false); // stop the "running" animation if we have an animator
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed; // change how fast the object moves
    }

    public void SetDistance(float newDistance)
    {
        moveDistance = newDistance; // change how far the object goes
    }
}