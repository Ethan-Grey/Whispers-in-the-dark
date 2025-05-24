using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PauseRunItJumpScare : MonoBehaviour
{
    [Header("Monster Setup")]
    public GameObject monster;
    public Transform player;
    public float chaseSpeed = 5f;

    [Header("Chase Timing")]
    public float delayBeforeChase = 2f;
    public float chaseDuration = 4f;

    [Header("Audio")]
    public AudioSource standingAudio;  // Audio when monster appears
    public AudioSource runningAudio;   // Audio when monster starts running

    private bool triggered = false;
    private NavMeshAgent agent;

    void Start()
    {
        if (monster != null)
        {
            monster.SetActive(false);
            agent = monster.GetComponent<NavMeshAgent>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            StartCoroutine(JumpscareSequence());
        }
    }

    IEnumerator JumpscareSequence()
    {
        monster.SetActive(true);
        monster.transform.LookAt(player);

        // Play standing audio
        if (standingAudio != null) standingAudio.Play();

        yield return new WaitForSeconds(delayBeforeChase);

        // Switch to running audio
        if (standingAudio != null && standingAudio.isPlaying) standingAudio.Stop();
        if (runningAudio != null) runningAudio.Play();

        // Start chase
        if (agent != null)
        {
            agent.speed = chaseSpeed;
            StartCoroutine(NavMeshChase());
        }
        else
        {
            StartCoroutine(SimpleChase());
        }

        yield return new WaitForSeconds(chaseDuration);

        // Stop all audio
        if (runningAudio != null && runningAudio.isPlaying) runningAudio.Stop();

        // Hide monster
        monster.SetActive(false);

        // Destroy trigger so it doesn't repeat
        Destroy(gameObject);
    }

    IEnumerator NavMeshChase()
    {
        float timer = 0f;
        while (timer < chaseDuration)
        {
            if (agent != null)
            {
                agent.SetDestination(player.position);
            }

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator SimpleChase()
    {
        float timer = 0f;
        while (timer < chaseDuration)
        {
            monster.transform.LookAt(player);
            monster.transform.position += monster.transform.forward * chaseSpeed * Time.deltaTime;

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
