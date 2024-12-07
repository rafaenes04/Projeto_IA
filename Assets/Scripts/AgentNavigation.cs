using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentNavigation : MonoBehaviour
{
    [SerializeField]
    private Vector3 desiredDestination;

    void Start()
    {
        GetComponent<NavMeshAgent>().destination = desiredDestination;


        
    }

}
