using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //pathfinding
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform projectile_spawn_point;
    public float projectileRangeShoot;    
    public float projectileRangeBoost;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float health = 1000; 
    public Text UIstr;

    private float start_health;
    private float curr_health;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        start_health = health;
        //walkPointSet = true;
    }

    void FixedUpdate()
    {
        curr_health = health / start_health * 100;
        curr_health = Mathf.Clamp(curr_health, 0, 100);
        UIstr.text = "Le enemy helth: " + curr_health.ToString() + "%\n" + health.ToString();
        if (health <= 0)
        {
            Destroy(gameObject);
            UIstr.text = "u hev dafeateds le big bad enema";    
        }
    
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    
        if (!playerInAttackRange && !playerInSightRange) Patrolling();
        if (!playerInAttackRange && playerInSightRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (walkPointSet == false) {
            SearchWalkPoint();
            Debug.Log("not set");
        }

        if (walkPointSet == true)
        {
            Debug.Log("set");
            agent.SetDestination(walkPoint);
        }
    
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f);
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        Debug.Log("search\n");
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.y + randomZ);
    
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("setting\n");
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, projectile_spawn_point.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileRangeShoot, ForceMode.Impulse);
            rb.AddForce(transform.up * projectileRangeBoost, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}