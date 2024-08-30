using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public NavMeshAgent agent;      // Reference to the NavMeshAgent component
    public Transform player;        // Reference to the player's transform
    public Animator animator;       // Reference to the Animator component

    void Start()
    {
        // Ensure the agent is attached and properly configured
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Set a reasonable stopping distance
        agent.stoppingDistance = 1.5f;

        // Set agent properties for better control
        agent.updateRotation = true; // Allows agent to rotate towards the target automatically
        agent.isStopped = true;      // Initially stop the agent
    }

    void Update()
    {
        // Handle chasing the player
        ChasePlayer();

        // Update animator parameters based on the agent's state
        UpdateAnimatorParameters();
    }

    void ChasePlayer()
    {
        // Set the destination to the player's position
        agent.SetDestination(player.position);

        // Check if the agent has a valid path and is moving
        if (agent.hasPath && agent.remainingDistance > agent.stoppingDistance)
        {
            agent.isStopped = false;  // Ensure the agent is moving
            animator.SetBool("IsMoving", true);
        }
        else
        {
            agent.isStopped = true;   // Stop the agent if close enough or path is invalid
            animator.SetBool("IsMoving", false);
        }
    }

    void UpdateAnimatorParameters()
    {
        // Update Speed parameter based on the agent's current velocity
        float normalizedSpeed = agent.velocity.magnitude / agent.speed; // Normalize speed to 0-1 range
        animator.SetFloat("Speed", normalizedSpeed);

        // Trigger attack when within stopping distance
        if (agent.remainingDistance <= agent.stoppingDistance && agent.hasPath && !agent.pathPending)
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }
}
