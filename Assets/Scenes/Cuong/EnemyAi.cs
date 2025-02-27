using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Animator animator; // Animator của enemy

    public float starWaitTime = 4f;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrolling;
    bool m_CaughtPlayer;

    public float attackRange = 2.5f; // Khoảng cách để tấn công

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrolling = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = starWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Lấy Animator

        Agent.isStopped = false;
        Agent.speed = speedWalk;
        Agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnviromentView();

        if (!m_IsPatrolling)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distanceToPlayer <= attackRange) // Nếu đủ gần để tấn công
            {
                Attack();
            }
            else
            {
                animator.SetBool("isAttacking", false); // Dừng tấn công
                Chasing(); // Tiếp tục truy đuổi
            }
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        animator.SetBool("isRunning", true); // Chạy
        animator.SetBool("isAttacking", false); // Không tấn công

        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            Agent.SetDestination(m_PlayerPosition);
        }
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrolling = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = starWaitTime;
                Agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Patroling()
    {
        animator.SetBool("isRunning", false); // Trạng thái đứng yên
        animator.SetBool("isAttacking", false); // Không tấn công

        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            Agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = starWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Attack()
    {
        animator.SetBool("isRunning", false); // Dừng chạy
        animator.SetBool("isAttacking", true); // Bắt đầu tấn công
        Stop(); // Dừng di chuyển trong lúc tấn công
    }

    public void Move(float speed)
    {
        Agent.isStopped = false;
        Agent.speed = speed;
    }

    public void Stop()
    {
        Agent.isStopped = true;
        Agent.speed = 0;
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        Agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void LookingPlayer(Vector3 player)
    {
        Agent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                Agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = starWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrolling = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }
}
