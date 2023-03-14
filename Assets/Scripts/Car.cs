using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if(agent.remainingDistance < 0.1f)
        {
            City.instance.carCount--;
            Destroy(this.gameObject);
        }
    }
}
