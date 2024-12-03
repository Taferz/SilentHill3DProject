using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private NavMeshAgent agent; // NavMeshAgent for movement
    private Animator animator; // Animator for controlling animations

    public float sightRange = 10.0f; // Distance at which the zombie sees the player
    public float attackRange = 2.0f; // Distance for attacking the player
    public LayerMask obstacleMask; // Layer mask for obstacles that can block line of sight
    public float movementSpeed = 3.5f; // Movement speed of the zombie

    private bool hasScreamed = false; // Track if the zombie has already screamed

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        agent.isStopped = true; // Initially stop the agent
        agent.speed = movementSpeed; // Set the movement speed
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // Check if the player is within sight range and line of sight
            if (distance <= sightRange && !hasScreamed && IsPlayerInLineOfSight())
            {
                Scream();
            }
            // If the zombie has screamed, it starts walking toward the player
            else if (hasScreamed && distance > attackRange)
            {
                WalkTowardsPlayer();
            }
        }
    }

    bool IsPlayerInLineOfSight()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Perform a raycast from the zombie's position to the player's position
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, sightRange, obstacleMask))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void Scream()
    {
        hasScreamed = true;
        agent.isStopped = true; // Stop movement during scream
        animator.SetTrigger("hasSeenPlayer"); // Trigger the scream animation
        Debug.Log("Zombie screams!");
    }

    void WalkTowardsPlayer()
    {
        if (player == null)
            return;

        // Set the player's position as the destination
        agent.SetDestination(player.position);
        Debug.Log("Setting destination to player position: " + player.position);

        // Resume movement
        agent.isStopped = false;
        Debug.Log("Agent is stopped: " + agent.isStopped);

        // animator.SetBool("isWalking", true); // Activate walk animation
        Debug.Log("Zombie is walking toward the player.");
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.position); // Draw a line to the player
        }

        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.red;
            foreach (Vector3 corner in agent.path.corners)
            {
                Gizmos.DrawSphere(corner, 0.2f); // Visualize path corners
            }
        }
    }
}