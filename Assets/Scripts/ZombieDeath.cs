using UnityEngine;

public class ZombieDeath : MonoBehaviour
{
    public int health = 100;
    private Animator animator; // Reference to the Animator component
    private bool isDying = false; // Track if the object is dying
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is not assigned.");
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is not assigned.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return; // Prevent taking damage if already dying

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else
        {
            // Trigger the hit animation
            if (animator != null)
            {
                animator.SetTrigger("Hit");
                Debug.Log("Hit animation triggered.");
            }
        }
    }

    private void Die()
    {
        if (isDying) return; // Prevent multiple calls to Die

        isDying = true;
        Debug.Log("Die method called. Triggering dying animation.");

        // Trigger the dying animation
        if (animator != null)
        {
            animator.SetBool("isDying", true);
            Debug.Log("isDying parameter set to true.");
        }

        // Disable further movement and actions
        DisableEnemy();
    }

    private void DisableEnemy()
    {
        // Disable the NavMeshAgent if it exists
        var navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        // Disable the collider to prevent further interactions
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Set the Rigidbody to kinematic to prevent falling through the ground
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Optionally, disable other components or scripts as needed
        // For example, disable the EnemyNavMesh script
        var enemyNavMesh = GetComponent<EnemyNavMesh>();
        if (enemyNavMesh != null)
        {
            enemyNavMesh.enabled = false;
        }
    }
}