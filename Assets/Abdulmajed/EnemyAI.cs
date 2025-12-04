using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Patrol points
    public Transform pointA;
    public Transform pointB;


    public Transform player;
    //movement speeds and patroling and chasing
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseRange = 8f;
    // the current patrol target
    Transform currentTarget;

    // enemy health
    public int maxHealth = 100;
    int currentHealth;

    NavMeshAgent agent;

    void Start()
    {
        //enemy starts patrolling towards point A
        currentTarget = pointA;
        //set enemy health to full at the start
        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.speed = patrolSpeed;
            agent.stoppingDistance = 0f;
            agent.SetDestination(currentTarget.position);

        }
        else
        {

            Debug.LogError("EnemyAI : navMeshAgent is missing on thes enemy ");


        }
    }

    void Update()
    {

        if (agent == null || player == null)
        {
            return;
        }
        // Calculate distance between enemy and player
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        // if player is close enough, chase them
        if (distToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }


        // update the agent's destination if patrolling
        void Patrol()
    {
            //check if the agent has reached its destination
            if (agent.pathPending)
            {
                return;
            }
            // if close enough to the target, switch to the other point
            if (agent.remainingDistance <= 0.2f)
            { 
            
                currentTarget = (currentTarget == pointA) ? pointB : pointA;
                agent.speed = patrolSpeed;
                agent.stoppingDistance = 0f;
                agent.SetDestination(currentTarget.position);

            }
    }

    void ChasePlayer()
    {
        //create a target position with Y fixed (to avoid vertical rotation)
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
      
       
            agent.speed = chaseSpeed;
            agent.stoppingDistance = 1.5f;
            agent.SetDestination(targetPos);
            //rotate to face the player
            LookAt(player.position);
    }

    void LookAt(Vector3 targetPos)
        {
            //get direction vector
            Vector3 dir = targetPos - transform.position;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.001f)
            {
                //calculate desired rotation
                Quaternion rot = Quaternion.LookRotation(dir);
                //smoothly rotate towards the target
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
            }
        }
    }
    //method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        
        //remove enemy from the scene
        Destroy(gameObject);
    }
}
