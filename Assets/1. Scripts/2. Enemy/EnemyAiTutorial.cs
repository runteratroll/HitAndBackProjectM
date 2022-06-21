using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;


    DevilBulldogAttack attacks;
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attacks = GetComponent<DevilBulldogAttack>();
    }

    private void Update()
    {
        //Check for sight and attack range
        if (alreadyAttacked == true) //공격중이라면
        {
            return;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    //스테이트패턴도 적용시켜보자
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            Debug.Log("움직이기");
            //SoundManager.instance.PlaySE("EnemyBulldogWalk");
            agent.SetDestination(walkPoint);
        }


        Vector3 distanceToWalkPoint = walkPoint - transform.position;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log("설정한 거리와 많이 가까워짐");
            walkPointSet = false;
        }

    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint + new Vector3(0f, 2f, 0f), -transform.up, 5f, whatIsGround)) //터레인 콜라이더설정과, walkPoint의 y좌표와 레이캐스트이 y좌표가 같아서
        {
            //충돌이 안됬구나
            //레이어 설정을 안했구나 ㅇ...
            Debug.Log("아래에서 쏘기 땅에 맞음");

            walkPointSet = true;
        }

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        Debug.Log("움직임 멈춤");
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);


        if (!alreadyAttacked)
        {
            Debug.Log("디버그어택");
            transform.LookAt(player);
            attacks.Attack(); //애니메이션과 Attack  isAttack실행
            alreadyAttacked = true;

        }
    }


    public void AlreadyAttack()
    {
        alreadyAttacked = false;
    }

    //애니메이션으로 정지 모션을 정하자

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawLine(walkPoint, -transform.up * 15f);
    }
}
