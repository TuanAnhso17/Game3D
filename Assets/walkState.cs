using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class walkState : StateMachineBehaviour
{
    List<Transform> WayPoints = new List<Transform>();
    NavMeshAgent agent;

    float time;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        agent = animator.gameObject.transform.parent.GetComponent<NavMeshAgent>();

        GameObject gameObject = GameObject.FindGameObjectWithTag("WayPoints");

        foreach (Transform tran in gameObject.transform)
            WayPoints.Add(tran);

        agent.SetDestination(WayPoints[Random.Range(0, WayPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time > 10)
            animator.SetBool("isPatrolling", false);

        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(WayPoints[Random.Range(0, WayPoints.Count)].position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}