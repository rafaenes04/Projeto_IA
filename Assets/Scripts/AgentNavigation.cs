using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentNavigation : MonoBehaviour
{
    [SerializeField]
    private Vector3 desiredDestination;

    private NavMeshAgent agent;

    public Vector3 DesiredDestination => desiredDestination; // Public getter.

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        if (desiredDestination != Vector3.zero)
        {
            SetDestination(desiredDestination);
        }
        else
        {
            Debug.LogError("Desired destination is not set properly.");
        }
    }

    public void SetDestination(Vector3 newDestination)
    {
         if (agent != null)
        {
            agent.ResetPath();
            agent.destination = desiredDestination; 
        }
    }
}
