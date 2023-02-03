using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Transform closestRoot = PlantManager.Instance.GetClosestRoot(transform.position);

        if (closestRoot != null)
        {
            agent.SetDestination(closestRoot.position);
        }

        // TODO: need something to happen here if no roots currently exist
    }
}
