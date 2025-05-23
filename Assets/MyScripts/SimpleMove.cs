using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 9f;
    public bool autoStart = false;

    private Animator animator;
    private float distanceMoved;
    private bool isMoving = false;
    private Vector3 startPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        
        if (autoStart)
        {
            StartMoving();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (distanceMoved < moveDistance)
            {
                float moveAmount = moveSpeed * Time.deltaTime;
                transform.position += transform.forward * moveAmount;
                distanceMoved += moveAmount;
            }
            else
            {
                StopMoving();
            }
        }
    }

    public void StartMoving()
    {
        if (!isMoving)
        {
            isMoving = true;
            distanceMoved = 0f;
            startPosition = transform.position;
            
            if (animator != null)
                animator.SetBool("isRunning", true);
        }
    }

    public void StopMoving()
    {
        isMoving = false;
        
        if (animator != null)
            animator.SetBool("isRunning", false);
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void SetDistance(float newDistance)
    {
        moveDistance = newDistance;
    }
}