using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamageable 
{
    // Patrol points
    public Transform pointA;
    public Transform pointB;
    // Player reference
    public Transform player;
    // Movement speeds
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    // Distances
    public float chaseRange = 8f;       
    public float chaseLoseRange = 15f;  
    // Vision
    public float viewAngle = 45f;     
    // Enemy health
    public float maxHealth = 100;
    float currentHealth;
    // Current patrol target
    Transform currentTarget;
    NavMeshAgent agent;
    bool isChasing = false;
    void Start()
    {
        // Enemy starts patrolling towards point A
        currentTarget = pointA;
        // Set enemy health to full at the start
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
            Debug.LogError("EnemyAI: NavMeshAgent is missing on this enemy");
        }
    }
    void Update()
    {
        if (agent == null || player == null)
            return;

        Vector3 dirToPlayer = player.position - transform.position;
        float distToPlayer = dirToPlayer.magnitude;
        bool canSeePlayer = false;
        if (!isChasing)
        {
            if (distToPlayer <= chaseRange)
            {
                Vector3 flatDir = dirToPlayer;
                flatDir.y = 0f;
                float angle = Vector3.Angle(transform.forward, flatDir);
                if (angle <= viewAngle * 0.5f)
                {            
                    RaycastHit hit;
                    Vector3 eyePos = transform.position + Vector3.up * 0.5f;
                    if (Physics.Raycast(eyePos, flatDir.normalized, out hit, chaseRange))
                    {
                        if (hit.transform == player)
                        {
                            canSeePlayer = true;
                        }
                    }
                }
            }

            if (canSeePlayer)
            {
                isChasing = true;
            }
        }
        else
        {
            if (distToPlayer > chaseLoseRange)
            {
                isChasing = false;
            }
        }
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // check if the agent has reached its destination
        if (agent.pathPending)
            return;
        // if close enough to the target, switch to the other point
        if (agent.remainingDistance <= 0.2f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;

            agent.speed = patrolSpeed;
            agent.stoppingDistance = 0f;
            agent.SetDestination(currentTarget.position);
        }
        LookAt(currentTarget.position);
    }
    void ChasePlayer()
    {
        // create a target position with Y fixed (to avoid vertical rotation)
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        agent.speed = chaseSpeed;
        agent.stoppingDistance = 1.5f;
        agent.SetDestination(targetPos);
        // rotate to face the player
        LookAt(player.position);
    }
    void LookAt(Vector3 targetPos)
    {
        // get direction vector
        Vector3 dir = targetPos - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude > 0.001f)
        {
            // calculate desired rotation
            Quaternion rot = Quaternion.LookRotation(dir);
            // smoothly rotate towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }
    }
    // method to apply damage to the enemy
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            onDeath();
        }
    }
  public  void onDeath()
    {
        // remove enemy from the scene
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        if (player == null)
            return;
        Vector3 eyePos = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = new Color(1f, 0.5f, 0f, 1f);
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.DrawRay(eyePos, transform.forward);

        float halfAngle = viewAngle * 0.5f;

        Vector3 leftDir = Quaternion.Euler(0, -halfAngle, 0) * transform.forward;
        Vector3 rightDir = Quaternion.Euler(0, halfAngle, 0) * transform.forward;
         
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(eyePos, leftDir * chaseRange);
        Gizmos.DrawRay(eyePos, rightDir * chaseRange);

        Gizmos.color = Color.red;
        Vector3 flatDir = player.position - transform.position;
        flatDir.y = 0f;

        Gizmos.DrawRay(eyePos, flatDir.normalized * chaseRange);


    }
}
