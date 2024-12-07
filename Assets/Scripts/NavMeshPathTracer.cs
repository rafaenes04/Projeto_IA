using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]



public class NavMeshPathTracer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private NavMeshAgent agent;
    public float pathYOffset = -0.1f; 


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 0;
    }


    void Update()
    {
        if (agent.hasPath)
        {
            DrawPath();
        }
    }

    void DrawPath()
    {
        NavMeshPath path = agent.path;
        lineRenderer.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            
            Vector3 position = path.corners[i];
            position.y += pathYOffset;
            lineRenderer.SetPosition(i, position);
        }
    }

}
