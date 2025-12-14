using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IDamageable
{
    // Patrol points
    public Transform pointA;
    public Transform pointB;
    // Player reference
    private Transform player;
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
    public float currentHealth;
    private bool isDead = false;
    // Current patrol target
    Transform currentTarget;
    NavMeshAgent agent;
    // Audio sources
    public AudioSource WalkSFX;
    public AudioSource PatrolSFX;
    public AudioSource ChaseSFX;
    public AudioSource TalkSFX;
    // Attack parameters
    public float attackRange = 3f;
    public float attackDamage = 10f;
    public float attackCooldown = 3f;
    [SerializeField] float Timer = 0f;
    //Animator
    private Animator _alienAnimator;
    private Rigidbody _alienRB;

    bool isChasing = false;
   
    bool DetectPlayed = false;

    private void Awake()
    {
        _alienAnimator = GetComponent<Animator>();
        _alienRB = GetComponent<Rigidbody>();
    }
    void Start()
    {
        _alienRB.freezeRotation = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Enemy starts patrolling towards point A
        currentTarget = pointA;
        // Set enemy health to full at the start
        currentHealth = maxHealth;
        // Get NavMeshAgent for movement
        agent = GetComponent<NavMeshAgent>();
        
        if (agent != null)
        {
            agent.updateRotation = false;   
            agent.speed = patrolSpeed;
            agent.stoppingDistance = 0.5f;
            agent.SetDestination(currentTarget.position);
        }
        else
        {
            Debug.LogError(" NavMeshAgent is missing on this enemy");
        }
    }
    void Update()
    {
        if (isDead) return;


        Timer += Time.deltaTime;
        // prevent AI logic from running if agent or player references is missing
        if (agent == null || player == null)
            return;

        Vector3 dirToPlayer = player.position - transform.position;
        float distToPlayer = dirToPlayer.magnitude;
        bool canSeePlayer = false;
        // patrol or chase logic
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
                            Debug.Log("I'm following player");
                        }
                    }
                }
            }
            // if player is visible, begin chase
            if (canSeePlayer)
            {
                if (!DetectPlayed && TalkSFX != null)
                {
                    TalkSFX.Play();
                    DetectPlayed = true;
                }
                isChasing = true;
            }
        }
        // chase mode 
        else
        {
            if (distToPlayer > chaseLoseRange)
            {
                isChasing = false;
                DetectPlayed = false; // detect sound can play again next time
                agent.speed = patrolSpeed;

                agent.SetDestination(currentTarget.position);// resume patrolling btween points A and B
            }
        }
        // execute state
        if (isChasing)
        {
            ChasePlayer();// run chase behavior
        }
        else
        {
            Patrol();// run patrol behavior
        }
        //attack player
        if (distToPlayer <= attackRange && Timer >= attackCooldown && isChasing)
        {
            _alienAnimator.SetTrigger("HitTrigger");
            player.GetComponent<IDamageable>().takeDamage(attackDamage);
            Timer = 0;
        }

            //Animator values updating
            _alienAnimator.SetBool("isStopped", agent.isStopped);
        _alienAnimator.SetBool("isChasing", isChasing);
    }
    // patrol behavior
    void Patrol()
    {
        agent.stoppingDistance = 0.5f;
        // check if the agent has reached its destination
        if (agent.pathPending)
            return;
        // if close enough to the target, switch to the other point
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;

            agent.speed = patrolSpeed;
            agent.SetDestination(currentTarget.position);
        }
        // smoothly rotate enymy to face movement direction
        LookAt(currentTarget.position);
        //sound effects
        if (!WalkSFX.isPlaying)
        {          
            WalkSFX.Play();
        }
        if (!PatrolSFX.isPlaying)
        {
            PatrolSFX.Play();
        }
        if (ChaseSFX.isPlaying)
        {
            ChaseSFX.Stop();
        }
    }
    // chase behavior
    void ChasePlayer()
    {
        // create a target position with Y fixed (to avoid vertical rotation)
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        agent.speed = chaseSpeed;
        agent.stoppingDistance = attackRange;
        agent.SetDestination(targetPos);
        // rotate to face the player
        LookAt(player.position);

        //Stop when the player has been reached
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        // sound effects
        if (!WalkSFX.isPlaying)
        {
            WalkSFX.Play();
        }
        if (!ChaseSFX.isPlaying)
        {
            ChaseSFX.Play();
        }
        if (PatrolSFX.isPlaying)
        {
            PatrolSFX.Stop();
        }
    }
             // look rotation
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

        if (!isChasing)
        {
            isChasing = true;
        }
    }
    public void onDeath()
    {
        if(isDead) return;

        // remove enemy from the scene & stop all actions
        agent.isStopped = true;
        agent.enabled = false;
        _alienAnimator.SetTrigger("DeathTrigger");
        isDead = true;
        Destroy(transform.parent.gameObject, 1f);
    }

    // debug gizmos
    void OnDrawGizmos()
    {
        if (player == null)
            return;
        Vector3 eyePos = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = new Color(1f, 0.5f, 0f, 1f);
        Gizmos.DrawWireSphere(transform.position, chaseLoseRange);

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

