using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // States
    public float sightRange;
    public bool playerInSightRange;

    private bool isChasing = false; // Track if the zombie is currently chasing the player

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        src.clip = sfx1; // Set the clip once in Awake
        src.loop = true; // Ensure the sound loops
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange)
        {
            if (isChasing)
            {
                isChasing = false;
                src.Stop(); // Stop the zombie sound when not chasing
                StartCoroutine(BGMManager.instance.AdjustPitchSmoothly(1.0f, 1f)); // Smoothly reset BGM pitch to normal
            }
            Patroling();
        }
        else
        {
            if (!isChasing)
            {
                isChasing = true;
                src.Play(); // Play the zombie sound when chasing
                StartCoroutine(BGMManager.instance.AdjustPitchSmoothly(0.75f, 1f)); // Smoothly slow down BGM pitch
            }
            ChasePlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}