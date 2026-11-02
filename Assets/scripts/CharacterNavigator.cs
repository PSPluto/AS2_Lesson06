using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class CharacterNavigator : MonoBehaviour
{
    [FormerlySerializedAs("target")] [SerializeField] private Transform[] targets;
    [SerializeField] private Transform player;
    public NavMeshAgent agent;
    private int _currentIndex = 0;
    private bool isChased = false;

    private void Start()
    {
        if (agent != null && targets != null && targets.Length > 0)
        {
            agent.SetDestination(targets[_currentIndex].position);
        }
    }

    private void Update()
    {
        if (isChased && player != null)
        {
            agent.SetDestination(player.position);
            return;
        }

        if ( FinishNavigation() )
        {
            SetNextPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player != null && (other.transform == player || other.transform.IsChildOf(player)))
        {
            isChased = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player != null && (other.transform == player || other.transform.IsChildOf(player)))
        {
            isChased = false;
        }
    }

    private bool FinishNavigation()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    private void SetNextPoint()
    {
        if (targets == null || targets.Length == 0)
        {
            return;
        }
        
        _currentIndex = ((_currentIndex + 1) % targets.Length);
        agent.SetDestination(targets[_currentIndex].position);
    }
}
