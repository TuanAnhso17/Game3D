using UnityEngine;
using UnityEngine.AI;

public class RunState : StateMachineBehaviour
{

    NavMeshAgent agent;
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.gameObject.transform.parent.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //agent.SetDestination(player.position);
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not active or not placed on a NavMesh.");
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance > 15)
            animator.SetBool("isChasing", false);
        if (distance < 2.5)
            animator.SetBool("isChasing", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }
}