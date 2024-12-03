using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walkState : StateMachineBehaviour
{
    float time;

    Transform player;
    float chaseRange = 16;

    List<Transform> waypoints = new List<Transform>();

    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        agent = animator.gameObject.transform.parent.GetComponent<NavMeshAgent>();
        GameObject gameObject = GameObject.FindGameObjectWithTag("Waypoint");
        foreach (Transform t in gameObject.transform)
        {
            waypoints.Add(t);
        }
        agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time > 10)
        {
            animator.SetBool("IsPatrolling", false);
        }
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance <= chaseRange)
        {
            animator.SetBool("IsChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

}