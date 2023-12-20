using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    public void Revive()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);
        print("walta");
        while(enabled)
        {
            Agent.SetDestination(Player.transform.position);
            yield return Wait;
        }
    }
}