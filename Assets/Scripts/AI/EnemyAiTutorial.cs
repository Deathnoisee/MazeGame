using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public AudioSource src;
    public float rotationSpeed = 500f;
    public AudioClip sfx1;
    public NavMeshAgent agent;
    public Animator animator;
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
        src.clip = sfx1; 
        src.loop = true; 
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (agent.isOnOffMeshLink)
        {
           StartCoroutine(HandleJump());
        }
        else
        {
            animator.SetBool("jump", false); // Stop the jump animation
        }
        if (!playerInSightRange)
        {
            if (isChasing)
            {
                isChasing = false;
                src.Stop(); // Stop the zombie sound when not chasing
                StartCoroutine(BGMManager.instance.AdjustPitchSmoothly(1.0f, 1f));
            }
            Patroling();
        }
        else
        {
            if (!isChasing)
            {
                isChasing = true;
                src.Play(); 
                StartCoroutine(BGMManager.instance.AdjustPitchSmoothly(0.75f, 1f)); 
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
    Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    NavMeshHit hit;
    if (NavMesh.SamplePosition(randomPoint, out hit, walkPointRange, NavMesh.AllAreas))
    {
        walkPoint = hit.position;
        walkPointSet = true;
    }
    else
    {
        walkPointSet = false; // If no valid point is found, try again in the next frame
    }
}

    private void ChasePlayer()
    {
    Vector3 directionToPlayer = (player.position - transform.position).normalized;

    // Check if there is a direct line of sight to the player
    if (!Physics.Raycast(transform.position, directionToPlayer, Vector3.Distance(transform.position, player.position), whatIsGround))
    {
        agent.SetDestination(player.position); // Move directly to player
    }
    else
    {
        // If path is blocked, find a valid alternative path
        if (agent.pathStatus == NavMeshPathStatus.PathPartial || agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            SearchWalkPoint(); // Patrol around until a clear path is found
        }
        else
        {
            agent.SetDestination(player.position); // Continue chasing
        }
    }
    }

        private IEnumerator HandleJump()
    {
    OffMeshLinkData linkData = agent.currentOffMeshLinkData; // Get jump data
    Vector3 startPos = agent.transform.position;
    Vector3 endPos = linkData.endPos + Vector3.up * agent.baseOffset; // Target landing position

    agent.isStopped = true;
    animator.SetBool("jump", true);

    float jumpDuration = 0.5f; // Adjust based on your animation
    float elapsedTime = 0f;

    while (elapsedTime < jumpDuration)
    {
        agent.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / jumpDuration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    agent.transform.position = endPos; // Ensure correct final position
    animator.SetBool("jump", false);
    agent.isStopped = false;
    agent.CompleteOffMeshLink();
}


}