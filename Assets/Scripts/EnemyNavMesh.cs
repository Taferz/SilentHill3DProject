using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    private NavMeshAgent agent; // NavMeshAgent for movement
    private Animator animator; // Animator for controlling animations
    private ZombieDeath zombieDeath; // Reference to the HealthSystem component

    public float sightRange = 10.0f; // Distance at which the agent sees the player
    public float attackRange = 2.0f; // Distance at which the agent attacks the player
    public float screamDuration = 2.0f; // Duration of the scream animation

    private float screamTimer = 0f; // Timer for scream duration
    private bool isScreaming = false; // Track if the agent is screaming
    private bool isDead = false; // Track if the agent is dead

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Ensure agent starts stopped
        agent.isStopped = true;
    }

    void Update()
    {
        if (player == null || isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Handle screaming
        if (isScreaming)
        {
            screamTimer += Time.deltaTime;
            if (screamTimer >= screamDuration)
            {
                isScreaming = false;
                screamTimer = 0f;
                animator.SetBool("isScreaming", false);
                agent.isStopped = false;
            }
            return; // Don't perform other actions while screaming
        }

        // Check if the player is in sight
        bool playerInSight = IsPlayerInSight();

        if (playerInSight)
        {
            if (distanceToPlayer > attackRange)
            {
                // Chase the player
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                // Attack the player
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
            }
        }
        else
        {
            // Player out of sight
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isScreaming", false);
        }
    }

    bool IsPlayerInSight()
    {
        if (Vector3.Distance(transform.position, player.position) <= sightRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, sightRange))
            {
                if (hit.transform == player)
                {
                    // Start screaming if not already
                    if (!isScreaming)
                    {
                        isScreaming = true;
                        animator.SetBool("isScreaming", true);
                        agent.isStopped = true; // Stop agent during scream
                    }
                    return true;
                }
            }
        }
        return false;
    }
}