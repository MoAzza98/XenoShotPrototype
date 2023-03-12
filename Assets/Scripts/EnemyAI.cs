using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround, whatisPlayer;
    public GameObject bullet;
    public EnemyHealth health;
    public WeaponManager weapon;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInaAttackRange;

    private void Awake()
    {
        weapon = GetComponentInChildren<WeaponManager>();
        health = GetComponent<EnemyHealth>();
        player = GameObject.Find("Soldier").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInaAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!playerInSightRange && !playerInaAttackRange) Patrolling();
        if (playerInSightRange && !playerInaAttackRange) ChasePlayer();
        if (!playerInSightRange && playerInaAttackRange) AttackPlayer();
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack Code here
            Rigidbody rb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if( distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.x + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
        {
            walkPointSet = true;
        }

    }

}
