using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    // patroling
    public UnityEngine.Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
   
   // States
    public float sightRange;
    public bool playerInSightRange, playerInAttackRange;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position , sightRange, whatIsPlayer);
        if(!playerInSightRange)
        {
            patroling();
        }
        else    
        {
            chasePlayer();
        }
    }

    private void patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        } 
        else 
        {
            agent.SetDestination(walkPoint);
        }
        UnityEngine.Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, +walkPointRange);
        float randomx = Random.Range(-walkPointRange, +walkPointRange);
        walkPoint = new UnityEngine.Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void chasePlayer ()
    {
        agent.SetDestination(player.position);

    }
    private void OGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
