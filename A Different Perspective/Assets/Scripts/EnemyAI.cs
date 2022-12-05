using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{


    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Rigidbody Player;
    //enemy pathing
    private EnemyState enemyState = EnemyState.patrol;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //others
    public Stats stats;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }



    private void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        switch (enemyState)
        {

            case EnemyState.patrol:
                Patrolling();

                if (playerInSightRange && !playerInAttackRange)
                {
                    enemyState = EnemyState.chase;
                }

                if (!playerInSightRange && playerInAttackRange)
                {
                    enemyState = EnemyState.attack;
                }
                break;

            case EnemyState.chase:
                ChasePlayer();
                if (!playerInSightRange && !playerInAttackRange)
                {
                    enemyState = EnemyState.patrol;
                }

                if (!playerInSightRange && playerInAttackRange)
                {
                    enemyState = EnemyState.attack;
                }

                break;

            case EnemyState.attack:
                AttackPlayer();

                if (!playerInSightRange && !playerInAttackRange)
                {
                    enemyState = EnemyState.patrol;
                }
                else if (playerInSightRange && !playerInAttackRange)
                {
                    enemyState = EnemyState.chase;
                }

                break;

            default:

                break;
        }

    }



    private void Patrolling()
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceTowalkPoint = transform.position - walkPoint;

        //if walkpoint reached
        if (distanceTowalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    void SearchWalkPoint()
    {
        //caculating random point in range
        float randZ = Random.Range(-walkPointRange, walkPointRange);
        float randX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        //Make sure enemy dosnt walk off map using raycast
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

   private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stats.health -= 10;
            KnockBack();

            if (stats.health <= 0)
            {
                stats.health = 0;
                Debug.Log("Death");
            }
        }   
    }

    private void KnockBack()
    {
        Player.AddForce((transform.up * 3), ForceMode.Impulse);
        Player.AddForce((-transform.forward * 60), ForceMode.Impulse);
    }
    public enum EnemyState
    {
        patrol,
        chase,
        attack,
    }
}