using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI behavior system for dungeon enemies with state machine.
/// Manages idle, patrol, chase, attack, and death states. Uses NavMesh for pathfinding and waypoint patrol.
/// Triggers battles when player enters attack range.
/// </summary>
public class EnemySimpleAI : MonoBehaviour
{
    enum EnemyState { Idle, Patrolling, Chasing, Attacking, Death }
    [SerializeField] EnemyState currentState = EnemyState.Idle;
    [SerializeField] float idleTime = 1.7f;
    [SerializeField] float chaseRange = 11f;
    [SerializeField] float attackRange = 7f;
    [SerializeField] float patrolSpeed = 1f;
    [SerializeField] float chaseSpeed = 3f;

    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private Animator enemyAnimator;

    private Transform player;
    private Transform currentWaypoint;
    private List<Transform> waypoints = new List<Transform>();
    private EnemyManager enemyManager;
    private NavMeshAgent navMeshAgent;
    public int prefabIndex; // Índice del prefab en la lista de prefabs
    private float timer = 0f;
    [SerializeField] private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        if (enemySprite == null)
        {
            enemySprite = GetComponentInChildren<SpriteRenderer>();
        }
        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponentInChildren<Animator>();
        }
        FindWaypoints();
        SetNextWaypoint();
    }

    /// <summary>
    /// Main update loop that runs the state machine and updates animation parameters.
    /// The enemy cycles through Idle, Patrolling, Chasing, Attacking, and Death states.
    /// </summary>
    void Update()
    {
        enemyAnimator.SetFloat("speed", navMeshAgent.velocity.magnitude);
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Death:
                Die();
                break;
        }
    }

    /// <summary>
    /// Idle state: The enemy waits stationary for a duration before resuming patrol.
    /// Checks if the player comes into detection range.
    /// </summary>
    private void Idle()
    {
        navMeshAgent.speed = 0;
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            timer = 0;
            SetNextWaypoint();
            currentState = EnemyState.Patrolling;
        }
        CheckForPlayer();
    }

    /// <summary>
    /// Patrol state: The enemy moves toward its waypoint at patrol speed.
    /// Returns to idle when reaching the waypoint. Checks for player proximity to switch to chase state.
    /// </summary>
    private void Patrol()
    {
        navMeshAgent.speed = patrolSpeed;
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            currentState = EnemyState.Idle;
        }
        CheckForPlayer();
        FlipCharacter();
    }

    /// <summary>
    /// Chase state: The enemy moves toward the player at chase speed.
    /// Transitions to Attack if the player is within attack range, or back to Idle if the player escapes.
    /// </summary>
    private void Chase()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(player.position);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > chaseRange)
        {
            currentState = EnemyState.Idle;
        }
        else if (distanceToPlayer < attackRange)
        {
            currentState = EnemyState.Attacking;
        }

        FlipCharacter();
    }

    /// <summary>
    /// Attack state: The enemy performs an attack animation on the player.
    /// Prevents repeated attacks by using the isAttacking flag. Stops NavMesh movement during the attack.
    /// </summary>
    private void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;

        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.ResetPath();
        enemyAnimator.SetTrigger("isAttacking");
    }

    /// <summary>
    /// Ends the current attack and transitions to Chase or Attack state based on player distance.
    /// Called by animation events at the end of the attack animation.
    /// </summary>
    public void EndAttack()
    {
        isAttacking = false;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < attackRange)
            currentState = EnemyState.Attacking;
        else
            currentState = EnemyState.Chasing;
    }

    /// <summary>
    /// Death state: Stops the enemy's movement and plays the death animation.
    /// The object is destroyed after the animation completes (1.7 seconds).
    /// </summary>
    private void Die()
    {
        navMeshAgent.isStopped = true;
        enemyAnimator.SetTrigger("isDeath");
        Destroy(gameObject, 1.7f);
    }

    /// <summary>
    /// Called when the enemy successfully hits the player.
    /// Triggers the player's hit reaction and saves enemy state before battle transition.
    /// This method is called via an animation event at the end of the attack animation.
    /// </summary>
    public void HitPlayer()
    {
        player.GetComponent<PlayerController>().ProcessHit();
        enemyManager.SaveDGEnemiesData(transform);
    }

    /// <summary>
    /// Checks if the player is within chase range and transitions to chase state if so.
    /// </summary>
    private void CheckForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseRange)
        {
            currentState = EnemyState.Chasing;
        }
    }

    /// <summary>
    /// Finds all waypoints tagged "Waypoint" in the scene and caches their transforms for patrolling.
    /// </summary>
    private void FindWaypoints()
    {
        waypoints.Clear();
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject waypoint in waypointObjects)
        {
            waypoints.Add(waypoint.transform);
        }
    }

    /// <summary>
    /// Selects a random waypoint from the list and sets it as the enemy's destination.
    /// Uses NavMesh pathfinding to navigate to the waypoint.
    /// </summary>
    private void SetNextWaypoint()
    {
        if (waypoints.Count == 0) return;

        currentWaypoint = waypoints[Random.Range(0, waypoints.Count)];
        navMeshAgent.SetDestination(currentWaypoint.position);
    }

    /// <summary>
    /// Flips the character sprite horizontally based on movement direction.
    /// The sprite faces left (180°) when moving left, and right (0°) when moving right.
    /// </summary>
    private void FlipCharacter()
    {
        Vector3 direction = navMeshAgent.destination - transform.position;
        if (direction.x != 0)
        {
            // If x > 0 (moving right), rotation is 0. 
            // If x < 0 (moving left), rotation is 180 on the Y axis.
            float targetYRotation = (direction.x < 0) ? 180f : 0f;

            // Apply rotation to the sprite's transform
            enemySprite.transform.localRotation = Quaternion.Euler(0, targetYRotation, 0);
        }
    }

    /// <summary>
    /// Forces the enemy into the Death state.
    /// Used when the player defeats the enemy in battle and the enemy needs to be removed from the dungeon.
    /// </summary>
    public void TriggerDeath()
    {
        currentState = EnemyState.Death;
    }
}

