using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDuration = 3f;

    private Animator animator;
    private float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        if (timer < moveDuration)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            timer += Time.deltaTime;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
