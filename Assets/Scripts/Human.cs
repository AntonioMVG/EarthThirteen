using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (agent.remainingDistance < 0.1f)
        {
            City.instance.humanCount--;
            Destroy(this.gameObject);
        }
    }
}
